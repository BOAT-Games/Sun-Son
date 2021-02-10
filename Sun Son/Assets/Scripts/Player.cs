using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _playerSpeed;
    [SerializeField] float _jumpHeight;
    [SerializeField] int _maxLightPoints;
    [SerializeField] float _maxDashTime = 1.0f;
    [SerializeField] float _dashSpeed = 1.0f;
    [SerializeField] float _dashStoppingSpeed = 0.1f;
    [SerializeField] float _dashDelay = 2f;
    [SerializeField] int _dashCost = 20;

    [SerializeField] GameObject _pointLight;

    [SerializeField] LightBar _lightBar;

    [SerializeField] TrailRenderer _trailRenderer; 
    private CharacterController _controller;
    private int _currentLightPoints;
    private Vector3 _playerVelocity;
    private bool _grounded;
    private float _gravityValue = Physics.gravity.y;
    private Camera _mainCamera;
    private Vector3 _moveDirection = Vector3.zero;
    private float _currentDashTime;
    private bool _isDashCooldown;

    private float _nextDashAvailable;
    private bool _dashed = false;

    private bool _sheilded = false;
    private bool _sAttack = false;
    private bool _gAttack = false;

    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_maxLightPoints);
        _lightBar.SetLightPoints(_maxLightPoints);
        _pointLight.GetComponent<LightPower>().SetMaxLightPoints(_currentLightPoints);
        _pointLight.GetComponent<LightPower>().SetLightPoints(_maxLightPoints);

        transform.forward = _mainCamera.transform.right;

        _currentDashTime = _maxDashTime;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _grounded = _controller.isGrounded;
        if (_grounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            anim.SetBool("IsJumping", false);
        }

        Sheild();
        SwordAttack();
        GunAttack();
        PlayerBasicMove();
        Jump();
        Dash();
        
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    void PlayerBasicMove()
    {
        float horiz = Input.GetAxis("Horizontal");
        _moveDirection = _mainCamera.transform.right * horiz;

        if (_moveDirection != Vector3.zero)
        {
            transform.forward = _moveDirection.normalized;
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        _controller.Move(_moveDirection * Time.deltaTime * _playerSpeed);
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && _grounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.5f * _gravityValue);
            anim.SetBool("IsJumping", true);
        }
    }

    void Dash()
    {
        if (Time.time >= _nextDashAvailable && _currentLightPoints >= _dashCost)
        {
            if ((Input.GetKeyDown("left shift") || Input.GetKey("left shift")) && !_dashed && _moveDirection != Vector3.zero)
            {
                _isDashCooldown = true;
                _currentDashTime = 0.0f;
                _dashed = true;
                _currentLightPoints = _lightBar.GetLightPoints();
                _currentLightPoints -= _dashCost;
                _trailRenderer.enabled = true;
                _lightBar.SetLightPoints(_currentLightPoints);
                _pointLight.GetComponent<LightPower>().SetLightPoints(_currentLightPoints);
                _nextDashAvailable = Time.time + _dashDelay;
                //Debug.Log(_nextDashAvailable);
            }
        }
        if(Input.GetKeyUp("left shift") || !Input.GetKey("left shift"))
            _dashed = false;

        if (_currentDashTime < _maxDashTime)
            _currentDashTime += _dashStoppingSpeed;
        else
        {
            _moveDirection = Vector3.zero;
            _trailRenderer.enabled = false;
        }

        _controller.Move(_moveDirection * Time.deltaTime * _dashSpeed);
    }

    void Sheild()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_sheilded)
        {
            _sheilded = true;
            anim.SetBool("IsShielding", true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && _sheilded)
        {
            _sheilded = false;
            anim.SetBool("IsShielding", false);
            
        }

    }

    void SwordAttack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_sAttack)
        {
            anim.SetBool("IsSwordAttack", true);
            _sAttack = true;
        }
        else if ( _sAttack)
        {
            anim.SetBool("IsSwordAttack", false);
            _sAttack = false;
        }
    }

    void GunAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_gAttack)
        {
            anim.SetBool("IsGunAttack", true);
            _gAttack = true;
        }
        else if (_gAttack)
        {
            anim.SetBool("IsGunAttack", false);
            _gAttack = false;
        }
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

    public int getMaxLightPoints() {return _maxLightPoints;}
    public int getCurrentLightPoints() {return _currentLightPoints;}
    public float getDashDelay() {return _dashDelay;}
    public bool getIsDashCooldown() {return _isDashCooldown;}
    public void setIsDashCooldown(bool cooldown) {_isDashCooldown = cooldown;}
 }
