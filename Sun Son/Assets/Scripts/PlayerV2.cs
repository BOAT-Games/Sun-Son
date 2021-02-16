using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerV2 : MonoBehaviour
{

    // Fields for movement characteristics
    [SerializeField] float _jumpHeight = 1;
    [SerializeField] float _sprintSpeed = 4;
    [SerializeField] float _airSpeed = 3;
    [SerializeField] float _xDecel = 0.1f;
    [SerializeField] Transform _headHitOrigin;
    private float _currentGravity;
    private float _gravityValue = Physics.gravity.y * 1.5f;
    private float _wallSlideG = Physics.gravity.y / 4;
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private float _playerSpeed;
    private bool _grounded;

    // Fields for dash ability
    [SerializeField] float _maxDashTime = 1.0f;
    [SerializeField] float _dashSpeed = 10.0f;
    [SerializeField] float _dashStoppingSpeed = 0.1f;
    [SerializeField] float _dashDelay = 1f;
    [SerializeField] int _dashCost = 20;
    [SerializeField] bool _canDash = true;
    private float _currentDashTime;
    private bool _isDashCooldown;
    private float _nextDashAvailable;

    // Fields for double jump
    [SerializeField] int _doubleJumpCost = 10;
    [SerializeField] int _maxJumps = 2;
    private int _currentJumps;
    private bool _canDoubleJump;
    private bool _hasDoubleJumped;

    //Fields for wall grab
    [SerializeField] Transform _wallGrabRayOrigin;
    [SerializeField] float _wallJumpControlsDelay = 0.5f;
    [SerializeField] float _wallJumpPower = 1;
    private bool _grabbingWall;
    private float _controlsAvailable;


    // Fields for Player Resources
    [SerializeField] int _maxLightPoints;
    private int _currentLightPoints;

    // Fields for player input
    private PlayerControls _input;
    private Vector2 _currentMovement;
    private bool _movementPressed;
    private bool _dashPressed;
    private bool _jumpPressed;
    private bool _letGoPressed;

    // Fields for Animations
    private Animator _anim;
    private int _isRunningHash;
    private int _isJumpingHash;
    private int _isAirborneHash;
    private int _isDashingHash;
    private int _isGrabbingWallHash;
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
            _currentMovement = ctx.ReadValue<Vector2>();
            _movementPressed = Mathf.Abs(_currentMovement.x) >= 0.1;
        };

        _input.CharacterControls.Movement.canceled += ctx =>
        {
            _currentMovement = Vector2.zero;
            _movementPressed = false;
        };

        _input.CharacterControls.Dash.performed += ctx => _dashPressed = ctx.ReadValueAsButton();

        _input.CharacterControls.Jump.performed += ctx => _jumpPressed = ctx.ReadValueAsButton();

        _input.CharacterControls.Let_Go.performed += ctx => _letGoPressed = ctx.ReadValueAsButton();
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
        
        _isRunningHash = Animator.StringToHash("isRunning");
        _isJumpingHash = Animator.StringToHash("isJumping");
        _isAirborneHash = Animator.StringToHash("isAirborne");
        _isDashingHash = Animator.StringToHash("isDashing");
        _isShieldingHash = Animator.StringToHash("isShielding");
        _isFiringRangedHash = Animator.StringToHash("isFiringRanged");
        _isMeleeingHash = Animator.StringToHash("isMeleeing");
        _isGrabbingWallHash = Animator.StringToHash("isGrabbingWall");

        _trailRenderer.enabled = _canDash;
        _currentDashTime = _maxDashTime;
        _canDoubleJump = false;

        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_maxLightPoints);
        _lightBar.SetLightPoints(_maxLightPoints);
        _pointLight.GetComponent<LightPower>().SetMaxLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_maxLightPoints);

        _currentGravity = _gravityValue;

        _mainCamera.GetComponent<GlowComposite>().Intensity = (float)_currentLightPoints / (float)_maxLightPoints;

    }

    // Update is called once per frame
    void Update()
    {

        bool isAirborne = _anim.GetBool(_isAirborneHash);

        if (!_grabbingWall)
        {
            if (!isAirborne && !_grounded)
            {
                _anim.SetBool(_isAirborneHash, true);
            }
            else if (isAirborne && _grounded)
            {
                _anim.SetBool(_isAirborneHash, false);
            }
        }


    }

    private void FixedUpdate()
    {
        handleAerialChecks();

        handleWallGrab();

        if (!_grabbingWall && Time.time > _controlsAvailable)
        {
            _playerVelocity.x = 0;
            handleDirection();
            handleMovement();
            handleJumping();
            if (_canDash)
            {
                handleDashing();
            }
        }
        else
        {
            handleJumping();
        }

        // Drop due to gravity after all other effects are applied
        _playerVelocity.y += _currentGravity * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void handleAerialChecks()
    {
        _grounded = _controller.isGrounded;

        RaycastHit hit;
        Ray headRay = new Ray(_headHitOrigin.position, this.transform.up);
        bool hitHead = Physics.Raycast(headRay, out hit, 0.2f);

        if (hitHead && !_grounded && !_grabbingWall && _playerVelocity.y > 0)
            _playerVelocity.y = 0;

        if (_playerVelocity.y < 0)
        {
            if (_grounded)
            {
                _playerVelocity.y = 0f;
                _currentJumps = 0;
            }

            if (_grabbingWall && _currentGravity != _wallSlideG)
            {
                _playerVelocity.y = 0;
                _currentGravity = _wallSlideG;
            }
            else if (!_grabbingWall)
            {
                _currentGravity = _gravityValue;
            }
        }

        if (Mathf.Abs(_playerVelocity.x) > 0)
        {
            _playerVelocity.x -= _xDecel * this.transform.forward.x;
        }

        if (_grounded)
        {
            _hasDoubleJumped = false;
            _controller.stepOffset = 0.3f;
        }
    }

    private void handleWallGrab()
    {
        RaycastHit hit;
        Ray wallGrabRay = new Ray(_wallGrabRayOrigin.position, this.transform.forward);
        bool wGRayHit = Physics.Raycast(wallGrabRay, out hit, Mathf.Infinity, 1 << 8);

        if (wGRayHit && hit.distance > 0.4f && hit.normal.normalized == -wallGrabRay.direction.normalized)
        {
            wGRayHit = false;
        }

        if (!_grounded && !_grabbingWall && wGRayHit && _movementPressed)
        {
            _currentDashTime = _maxDashTime;
            _trailRenderer.enabled = false;
            _anim.SetBool(_isGrabbingWallHash, true);
            _anim.SetBool(_isAirborneHash, false);
            _currentJumps = 0;
            _grabbingWall = true;
        }
        else if (!wGRayHit || _grounded || (_letGoPressed && (_currentMovement.x * wallGrabRay.direction.x) <= 0))
        {
            _currentGravity = _gravityValue;
            _anim.SetBool(_isGrabbingWallHash, false);
            _grabbingWall = false;
        }
    }

    void handleDirection()
    {
        if (_currentMovement.x > 0)
            this.transform.forward = _mainCamera.transform.right;
        else if (_currentMovement.x < 0)
            this.transform.forward = -_mainCamera.transform.right;
    }

    void handleMovement()
    {
        _moveDirection = new Vector3(_currentMovement.x, 0, 0);
        bool isRunning = _anim.GetBool(_isRunningHash);

        if (!_movementPressed)
            _playerSpeed = 0;

        if(_movementPressed && !isRunning)
        {
            _anim.SetBool(_isRunningHash, true);
        }

        if(!_movementPressed && isRunning)
        {
            _anim.SetBool(_isRunningHash, false);
        }

        if (_grounded && isRunning)
        {
            _playerSpeed = Mathf.Lerp(_playerSpeed, _sprintSpeed, 0.2f);
        }
        else
        {
            _playerSpeed = Mathf.Lerp(_playerSpeed, _airSpeed, 0.2f);
        }

        if(isRunning)
            _controller.Move(_moveDirection * Time.deltaTime * _playerSpeed);
    }

    void handleJumping()
    {
        if (_jumpPressed && (_grounded || _grabbingWall))
        {
            ExecuteJump();
        }

        if (_jumpPressed && !_grounded && _currentJumps < _maxJumps && _canDoubleJump && _currentLightPoints >= _doubleJumpCost) 
        {
            ExecuteJump();
            _currentLightPoints -= _doubleJumpCost;
            _lightBar.SetLightPoints(_currentLightPoints);
            _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
            _hasDoubleJumped = true;
        }

    }

    void ExecuteJump() 
    {
        _controller.stepOffset = 0.0f;

        if (!_grounded)
            _playerVelocity.y = 0;

        if (_grabbingWall)
        {
            _playerVelocity.x += -this.transform.forward.x * _wallJumpPower;
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.5f * _gravityValue);

            _grabbingWall = false;
            _controlsAvailable = Time.time + _wallJumpControlsDelay;
            _anim.SetTrigger(_isJumpingHash);
            _currentJumps++;

            this.transform.forward = -this.transform.forward;
        }
        else if(!_grabbingWall)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.5f * _gravityValue);
            _anim.SetTrigger(_isJumpingHash);
            _currentJumps++;
        }

        // At the end of the Jump execution we tell the player that jump is no longer pressed, the next time
        // that jump can be set to true is after a full release and then re-pressing of the jump button
        _jumpPressed = false;
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
                _mainCamera.GetComponent<GlowComposite>().Intensity = (float)_currentLightPoints / (float)_maxLightPoints;

            }
        }
    }

    public void TakeDamage(int damage)
    {
        _currentLightPoints -= damage;
        _lightBar.SetLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
        _mainCamera.GetComponent<GlowComposite>().Intensity = (float)_currentLightPoints / (float)_maxLightPoints;

    }


    public int getMaxLightPoints() { return _maxLightPoints; }
    public int getCurrentLightPoints() { return _currentLightPoints; }
    public float getDashDelay() { return _dashDelay; }
    public bool getIsDashCooldown() { return _isDashCooldown; }
    public bool getCanDoubleJump() { return _canDoubleJump; }
    public bool getHasDoubleJumped() { return _hasDoubleJumped; }
    public bool getGrounded() { return _grounded; }
    public void setIsDashCooldown(bool cooldown) { _isDashCooldown = cooldown; }
    public void setCanDoubleJump(bool doublejump) { _canDoubleJump = doublejump;}

    void OnDrawGizmosSelected()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_wallGrabRayOrigin.position, this.transform.forward * 0.4f);

        Gizmos.DrawRay(_headHitOrigin.position, this.transform.up * 0.2f);
    }
}
