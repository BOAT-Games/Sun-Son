using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerV2 : MonoBehaviour
{

    // Fields for movement characteristics
    [SerializeField] float _jumpHeight = 1;
    [SerializeField] float _walkSpeed = 2;
    [SerializeField] float _sprintSpeed = 4;
    private float _gravityValue = Physics.gravity.y;
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private float _playerSpeed;
    private bool _grounded;

    // Fields for dash ability
    [SerializeField] float _maxDashTime = 1.0f;
    [SerializeField] float _dashSpeed = 10.0f;
    [SerializeField] float _dashStoppingSpeed = 0.1f;
    [SerializeField] float _dashDelay = 2f;
    [SerializeField] int _dashCost = 20;
    private float _currentDashTime;
    private bool _isDashCooldown;
    private float _nextDashAvailable;

    // Fields for Player Resources
    [SerializeField] int _maxLightPoints;
    private int _currentLightPoints;

    // Fields for player input
    private PlayerControls _input;
    private float _currentMovement;
    private bool _movementPressed;
    private bool _runPressed;
    private bool _dashPressed;
    private bool _jumpPressed;

    // Fields for Animations
    private Animator _anim;
    private int _isWalkingHash;
    private int _isRunningHash;
    private int _isJumpingHash;
    private int _isFallingHash;
    private int _isDashingHash;
    private int _isShieldingHash;
    private int _isFiringRangedHash;
    private int _isMeleeingHash;

    // Fields for FX
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] GameObject _pointLight;

    // Fields for UI Elements
    [SerializeField] LightBar _lightBar;

    // Main Camera
    private Camera _mainCamera;



    private void Awake()
    {
        _input = new PlayerControls();

        _input.CharacterControls.Movement.performed += ctx =>
        {   
            Vector2 temp = ctx.ReadValue<Vector2>();
            _currentMovement = temp.x;
            _movementPressed = _currentMovement != 0;
        };

        _input.CharacterControls.Dash.performed += ctx => _dashPressed = ctx.ReadValueAsButton();

        _input.CharacterControls.Run.performed += ctx => _runPressed = ctx.ReadValueAsButton();

        _input.CharacterControls.Jump.performed += ctx => _jumpPressed = ctx.ReadValueAsButton();
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
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        _isWalkingHash = Animator.StringToHash("isWalking");
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isFallingHash = Animator.StringToHash("isFalling");
        _isDashingHash = Animator.StringToHash("isDashing");
        _isShieldingHash = Animator.StringToHash("isShielding");
        _isFiringRangedHash = Animator.StringToHash("isFiringRanged");
        _isMeleeingHash = Animator.StringToHash("isMeleeing");

        _currentDashTime = _maxDashTime;

        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_maxLightPoints);
        _lightBar.SetLightPoints(_maxLightPoints);
        _pointLight.GetComponent<LightPower>().SetMaxLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_maxLightPoints);

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {

        _grounded = _controller.isGrounded;
        if (_grounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            _anim.SetBool(_isFallingHash, false);
        }
        else if(!_grounded && _playerVelocity.y < 0)
        {
            _anim.SetBool(_isFallingHash, true);
        }

        handleDirection();
        handleGroundMovement();
        handleJumping();
        handleDashing();

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    void handleDirection()
    {
        if (_currentMovement > 0)
            this.transform.forward = _mainCamera.transform.right;
        else if (_currentMovement < 0)
            this.transform.forward = -_mainCamera.transform.right;
    }

    void handleGroundMovement()
    {
        _moveDirection = _mainCamera.transform.right * _currentMovement;
        bool isRunning = _anim.GetBool(_isRunningHash);
        bool isWalking = _anim.GetBool(_isWalkingHash);

        if (!_movementPressed)
            _playerSpeed = 0;

        if(_movementPressed && !isWalking)
        {
            _anim.SetBool(_isWalkingHash, true);
        }

        if(!_movementPressed && isWalking)
        {
            _anim.SetBool(_isWalkingHash, false);
        }

        if((_movementPressed && _runPressed) && !isRunning)
        {
            _anim.SetBool(_isRunningHash, true);
        }

        if (!(_movementPressed && _runPressed) && isRunning)
        {
            _anim.SetBool(_isRunningHash, false);
        }

        if (isRunning)
            _playerSpeed = Mathf.Lerp(_playerSpeed, _sprintSpeed, 0.2f);
        else if (isWalking)
            _playerSpeed = Mathf.Lerp(_playerSpeed, _walkSpeed, 0.2f);

        if (isRunning || isWalking)
            _controller.Move(_moveDirection * Time.deltaTime * _playerSpeed);
    }

    void handleJumping()
    {
        if(_jumpPressed && _grounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.5f * _gravityValue);
            _anim.SetTrigger(_isJumpingHash);
        }
    }

    void handleDashing()
    {
        if (Time.time >= _nextDashAvailable && _currentLightPoints >= _dashCost)
        {
            if (_dashPressed && _movementPressed)
            {
                _anim.SetTrigger(_isDashingHash);
                _isDashCooldown = true;
                _currentDashTime = 0.0f;
                TakeDamage(_dashCost);
                _trailRenderer.enabled = true;
                _nextDashAvailable = Time.time + _dashDelay;
            }
        }

        if (_currentDashTime < _maxDashTime)
            _currentDashTime += _dashStoppingSpeed;
        else
        {
            _moveDirection = Vector3.zero;
            _trailRenderer.enabled = false;
        }

        _controller.Move(_moveDirection * Time.deltaTime * _dashSpeed);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Light"))
        {
            if (_currentLightPoints != _maxLightPoints)
            {
                _currentLightPoints = _maxLightPoints;
                _lightBar.SetLightPoints(_currentLightPoints);
                _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _currentLightPoints -= damage;
        _lightBar.SetLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
    }

    public int getMaxLightPoints() { return _maxLightPoints; }
    public int getCurrentLightPoints() { return _currentLightPoints; }
    public float getDashDelay() { return _dashDelay; }
    public bool getIsDashCooldown() { return _isDashCooldown; }
    public void setIsDashCooldown(bool cooldown) { _isDashCooldown = cooldown; }
}
