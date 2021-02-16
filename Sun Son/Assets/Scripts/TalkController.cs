using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkController : MonoBehaviour
{
    [SerializeField] TalkText _talkText;
    [SerializeField] PlayerV2 _playerV2;

    private PlayerControls _input;
    private bool _interactPressed;

    private bool convoStarted = false;
    // Start is called before the first frame update
    private void Awake()
    {
        _input = new PlayerControls();
        _input.CharacterControls.Interact.performed += ctx => _interactPressed = ctx.ReadValueAsButton();
        _input.CharacterControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("interact: " + _interactPressed);
        if ((_interactPressed || convoStarted) && _playerV2.getCanTalk())
        {
            _talkText.updateText("Hey Sun");
            convoStarted = true;
        }
        else if (_playerV2.getCanTalk() && !convoStarted)
        {
            _talkText.updateText("Press E to talk.");
        }
        else
        {
            _talkText.updateText("");
            convoStarted = false;
        }

    }
}
