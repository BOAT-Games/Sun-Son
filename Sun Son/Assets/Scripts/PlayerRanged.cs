using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged : MonoBehaviour
{
    private PlayerControls _input;
    private bool _attackPressed;

    private CharacterController _controller;

    [SerializeField] Transform _ShootPoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] int _shootCost = 1;

    private PlayerResources _pr;

    private Animator _anim;
    private int _isFiringRangedHash;

    public bool _isAttacking = false;

    private PlayerV2 _player;

    void Awake()
    {
        _input = new PlayerControls();

        _input.CharacterControls.Attack.performed += ctx => _attackPressed = ctx.ReadValueAsButton();
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
        _isFiringRangedHash = Animator.StringToHash("isFiringRanged");

        _controller = GetComponent<CharacterController>();

        _pr = FindObjectOfType<PlayerResources>();

        _player = gameObject.GetComponent<PlayerV2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.areControlsLocked())
        {
            if (_anim.GetBool("CanAttack"))
            {
                if (_attackPressed && !_anim.GetBool("isGrabbingWall") && !_anim.GetBool("isShielding"))
                {
                    Attack();
                }
            }
        }
        _attackPressed = false;
    }

    void Attack()
    {
        _anim.SetTrigger(_isFiringRangedHash);

        if(_controller.isGrounded)
            _anim.SetFloat("moveInputValue", 0.0f);

    }

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _ShootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 1000);
        _pr.TakeSelfDamage(_shootCost);
        Destroy(bullet, 0.5f);
    }
}
