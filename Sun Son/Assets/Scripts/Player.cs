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
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        _currentLightPoints = _maxLightPoints;

        transform.forward = _mainCamera.transform.right;

        _currentDashTime = _maxDashTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _grounded = _controller.isGrounded;
        if (_grounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }


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
        }

        _controller.Move(_moveDirection * Time.deltaTime * _playerSpeed);
    }

    void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && _grounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
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
                _currentLightPoints -= _dashCost;
                _trailRenderer.enabled = true;
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

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Light"))
        {
            if (_currentLightPoints != _maxLightPoints)
            {
                _currentLightPoints = _maxLightPoints;
            }
        }
    }

    public int getMaxLightPoints() {return _maxLightPoints;}
    public int getCurrentLightPoints() {return _currentLightPoints;}
    public float getDashDelay() {return _dashDelay;}
    public bool getIsDashCooldown() {return _isDashCooldown;}
    public void setIsDashCooldown(bool cooldown) {_isDashCooldown = cooldown;}
 }
