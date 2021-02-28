using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Interactable : MonoBehaviour
{
    private PlayerControls _input;
    private bool _interactPressed;

    private bool _isInRange;
    [SerializeField] TextMeshProUGUI _text; 
    [SerializeField] KeyCode _interactKey;
    [SerializeField] UnityEvent _interactAction;

    public void Awake() {
        _input = new PlayerControls();
        _input.CharacterControls.Interact.performed += ctx => _interactPressed = ctx.ReadValueAsButton();
 
    }

    private void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }
    void Update() 
    {
        if (_isInRange)
        {
            if (_interactPressed) 
            {
                _interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            _isInRange = true;
            _text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            _isInRange = false;
            _text.gameObject.SetActive(false);
        }
    }
}
