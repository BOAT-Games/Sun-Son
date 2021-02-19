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

    //Patroling
    [SerializeField] Transform[] targets;

    private int currentTarget = 0;

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

    private void Awake()
    {
        player = FindObjectOfType<PlayerV2>().gameObject.transform;
        agent.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(targets[currentTarget].position);

        _anim = GetComponent<Animator>();

        _isChasing = Animator.StringToHash("Chasing");
        _isAttacking = Animator.StringToHash("RangedAttack");

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
           // Patroling();
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

    private void Patroling()
    {
        if (Vector3.Distance(transform.position, targets[currentTarget].position) < 0.2f)
        {
            currentTarget++;
            if (currentTarget == targets.Length)
            {
                currentTarget = 0;
            }
        }
        agent.SetDestination(targets[currentTarget].position);
        agent.speed = 1;
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

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
