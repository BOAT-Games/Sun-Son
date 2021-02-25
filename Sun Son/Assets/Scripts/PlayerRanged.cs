using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRanged : MonoBehaviour
{
    private PlayerControls _input;
    private bool _attackPressed;

    [SerializeField] Transform _ShootPoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _attackRate = 0.6f;
    [SerializeField] int _shootCost = 1;

    private PlayerResources _pr;

    private Animator _anim;
    private int _isFiringRangedHash;

    public bool _isAttacking = false;
    private float _nextAttackTime = 0f;

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

        _pr = FindObjectOfType<PlayerResources>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            _isAttacking = false;
            _anim.SetBool(_isFiringRangedHash, false);

            if (_attackPressed && !_anim.GetBool("isGrabbingWall"))
            {
                Attack();
                _isAttacking = true;
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    void Attack()
    {
        _anim.SetBool(_isFiringRangedHash, true);

    }

    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _ShootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.forward * 1000);
        _pr.TakeDamage(_shootCost);
    }
}
