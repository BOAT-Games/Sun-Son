using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{

    private PlayerControls _input;
    private bool _attackPressed;

    [SerializeField] GameObject _sword;

    private Animator _anim;
    private int _isMeleeHash;

    public bool _isAttacking = false;

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

        if (_anim.GetBool("CanAttack"))
        {

            if (_attackPressed && !_anim.GetBool("isGrabbingWall"))
            {
                Attack();
            }
        }

        _attackPressed = false;
    }

    private void Attack()
    {
        _anim.SetTrigger(_isMeleeHash);
        _sword.SetActive(true);
        _attackPressed = false;
    }

    private void disableSword()
    {
        _sword.SetActive(false);
    }
}
