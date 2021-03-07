using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private PlayerControls _input;
    public bool _shieldPressed;

    [SerializeField] GameObject _shield;
    [SerializeField] GameObject _effectPrefab;
    [SerializeField] Transform _effectPoint;

    private PlayerSoundManager _psm;
    private bool _stopInitiated = false;

    private Animator _anim;
    private int _isShieldingHash;

    void Awake()
    {
        _input = new PlayerControls();

        _input.CharacterControls.Shield.performed += ctx => _shieldPressed = ctx.ReadValueAsButton();

        _input.CharacterControls.Shield.canceled += ctx => _shieldPressed = false;
    }

    private void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _isShieldingHash = Animator.StringToHash("isShielding");

        _shield.SetActive(false);
        _shield.transform.localScale = new Vector3(0f, 0f, 0f);

        _psm = GetComponent<PlayerSoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_shieldPressed)
        {
            if (!_shield.activeInHierarchy)
            {
                _shield.SetActive(true);
                _anim.SetBool(_isShieldingHash, true);
                _psm.playShield();
            }

            _shield.transform.localScale = Vector3.Slerp(_shield.transform.localScale, new Vector3(1, 3, 3), 0.05f);
        }
        else if(_shield.activeInHierarchy)
        {
            if (!_stopInitiated)
            {
                _psm.stopShield();
                _stopInitiated = true;
            }

            _shield.transform.localScale = Vector3.Slerp(_shield.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), 0.05f);


            if (_shield.transform.localScale.x <= 0.1)
            {
                _shield.SetActive(false);
                _anim.SetBool(_isShieldingHash, false);
                _stopInitiated = false;
            }
        }
    }

    public void ShieldImpact()
    {
        Instantiate(_effectPrefab, _effectPoint);
        _psm.playShieldImpact();
    }
}
