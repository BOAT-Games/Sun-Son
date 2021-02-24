using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// code based off of this video: https://www.youtube.com/watch?v=UjkSFoLxesw
public class ShooterController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] float health;

    //Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Animations
    private Animator _anim;
    private int _isChasing;
    private int _isAttacking;

    public SkinnedMeshRenderer rhead;
    public SkinnedMeshRenderer rbody;
    public SkinnedMeshRenderer rlegs;
    public Material red;
    private Material originalMaterial;

    private void Awake()
    {
        player = FindObjectOfType<PlayerV2>().gameObject.transform;
        agent.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(transform.position);

        _anim = GetComponent<Animator>();

        _isChasing = Animator.StringToHash("Chasing");
        _isAttacking = Animator.StringToHash("RangedAttack");

        originalMaterial = rhead.material;

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < sightRange)
        {
            playerInSightRange = true;
        }
        else
        {
            playerInSightRange = false;
        }

        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            playerInAttackRange = true;
        }
        else
        {
            playerInAttackRange = false;
        }

        if (!playerInSightRange && !playerInAttackRange)
        {
            _anim.SetBool(_isChasing, false);
            _anim.SetBool(_isAttacking, false);
            agent.SetDestination(transform.position);
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            _anim.SetBool(_isChasing, true);
            _anim.SetBool(_isAttacking, false);
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            if (!_anim.GetBool(_isAttacking))
            {
                _anim.SetBool(_isAttacking, true);
                _anim.SetBool(_isChasing, false);
            }
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = 3;
    }

    private void AttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);
    }

    public void Shoot()
    {
        Vector3 targetPostition = new Vector3(player.position.x,
                                        this.transform.position.y,
                                        player.position.z);
        transform.LookAt(targetPostition);

        Vector3 shootPoint = new Vector3(transform.position.x,
                               transform.position.y + 1, transform.position.z);
        //attack code here
        Rigidbody rb = Instantiate(projectile, shootPoint, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
        rb.AddForce(transform.up * 4f, ForceMode.Impulse);

        gameObject.GetComponent<AudioSource>().Play();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        rhead.materials = new Material[] { red, red, red };
        rbody.materials = new Material[] { red, red };
        rlegs.materials = new Material[] { red, red };

        Invoke("ResetColor", 0.1f);

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    void ResetColor()
    {
        rhead.materials = new Material[] { originalMaterial, originalMaterial, originalMaterial };
        rbody.materials = new Material[] { originalMaterial, originalMaterial };
        rlegs.materials = new Material[] { originalMaterial, originalMaterial };
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("Damaged");
            this.TakeDamage(other.gameObject.GetComponent<Weapon>()._damage);
        }
    }

}
