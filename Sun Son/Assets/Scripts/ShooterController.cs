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
    [SerializeField] float _decisionDelay = 3f;
    [SerializeField] Transform[] targets;

    private int currentTarget = 0;

    //Attacking
    [SerializeField] float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = FindObjectOfType<PlayerV2>().gameObject.transform;
        agent.GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(targets[currentTarget].position);

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
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
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
        Vector3 targetPostition = new Vector3(player.position.x,
                                        this.transform.position.y,
                                        player.position.z);
        transform.LookAt(targetPostition);

        if(!alreadyAttacked)
        {
            //attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 16f, ForceMode.Impulse);
            rb.AddForce(transform.up * 4f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
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
