using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//majority of code from:
//https://unity.grogansoft.com/navigation-with-the-nav-mesh-part-4-patrolling-and-chasing/

public class RatController : MonoBehaviour
{
    [SerializeField] int _damageCost = 2;
    [SerializeField] float _targetRange = 5;
    [SerializeField] float _decisionDelay = 3f;
    [SerializeField] Transform[] targets;
    [SerializeField] EnemyStates currentState;
    [SerializeField] Transform objectToChase;

    private GameObject _player;
    private float damageTimer = 1.0f;
    private int currentTarget = 0;

    
    NavMeshAgent agent;

    enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("SetDestination", 1.5f, _decisionDelay);
        //agent.SetDestination(targets.position);
        _player = FindObjectOfType<PlayerV2>().gameObject;

        if (currentState == EnemyStates.Patrolling)
        {
            agent.SetDestination(targets[currentTarget].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, objectToChase.position) > _targetRange)
        {
            currentState = EnemyStates.Patrolling;
            agent.speed = 1;
        }
        else
        {
            currentState = EnemyStates.Chasing;
            agent.speed = 2.5f;
        }

        if (currentState == EnemyStates.Patrolling)
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
        }
    }

    void SetDestination()
    {
        if (currentState == EnemyStates.Chasing)
        {
            agent.SetDestination(objectToChase.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageTimer <= 0)
            {
                _player.GetComponent<PlayerV2>().TakeDamage(_damageCost);
                damageTimer = 1.0f;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }
}
