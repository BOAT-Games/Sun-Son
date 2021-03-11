using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    private PlayerControls _input;
    private bool _attackPressed;

    private CharacterController _controller;

    [SerializeField] GameObject _sword;

    private Animator _anim;
    private int _isMeleeHash;

    public bool _isAttacking = false;
    public bool _hit = false;

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
        _isMeleeHash = Animator.StringToHash("isMeleeing");

        _controller = GetComponent<CharacterController>();

        _sword.SetActive(false);
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

    private void Attack()
    {
        _anim.SetTrigger(_isMeleeHash);

        if (_controller.isGrounded)
            _anim.SetFloat("moveInputValue", 0.0f); _sword.SetActive(true);

        _attackPressed = false;
        _isAttacking = true;
    }

    private void disableSword()
    {
        _isAttacking = false;
        _hit = false;
        _sword.SetActive(false);
    }

    public void Hit()
    {
        //Debug.Log("Hi");
        _hit = true;
    }
}
