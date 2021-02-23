using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    private PlayerControls _input;
    private bool _attackPressed;

    [SerializeField] GameObject _sword;
    [SerializeField] float _attackRate = 2;

    private Animator _anim;
    private int _isMeleeHash;

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
        _isMeleeHash = Animator.StringToHash("isMeleeing");

        _sword.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            _isAttacking = false;
            _anim.SetBool(_isMeleeHash, false);
            _sword.SetActive(false);
            if (_attackPressed)
            {
                Attack();
                _isAttacking = true;
                _nextAttackTime = Time.time + 1f / _attackRate;
            }
        }
    }

    private void Attack()
    {
        _anim.SetBool(_isMeleeHash, true);
        _sword.SetActive(true);
    }
}
