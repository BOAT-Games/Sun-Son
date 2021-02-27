using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//majority of code from:
//https://unity.grogansoft.com/navigation-with-the-nav-mesh-part-4-patrolling-and-chasing/

public class CrawlerController : MonoBehaviour
{
    [SerializeField] int _damageCost = 2;
    [SerializeField] float _targetRange = 5;
    [SerializeField] float _attackRange = 0.5f;
    [SerializeField] float _decisionDelay = 3f;

    [SerializeField] float _health = 0;


    [SerializeField] Transform[] targets;
    [SerializeField] EnemyStates currentState;

    private GameObject _player;
    private int currentTarget = 0;

    
    NavMeshAgent agent;

    private Animator _anim;
    private int _isWalkingHash;
    private int _isAttackingHash;

    public SkinnedMeshRenderer rbody;
    public Material red;
    private Material originalMaterial;



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

        _anim = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isAttackingHash = Animator.StringToHash("IsAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > _targetRange)
        {
            currentState = EnemyStates.Patrolling;
            _anim.SetBool(_isWalkingHash, true);
            _anim.SetBool(_isAttackingHash, false);
        }
        else if (Vector3.Distance(transform.position, _player.transform.position) < _attackRange)
        {
            agent.SetDestination(transform.position);
            _anim.SetBool(_isWalkingHash, false);
            _anim.SetBool(_isAttackingHash, true);
        }
        else
        {
            currentState = EnemyStates.Chasing;
            _anim.SetBool(_isWalkingHash, true);
            _anim.SetBool(_isAttackingHash, false);
        }

        if (currentState == EnemyStates.Patrolling)
        {
            if (Vector3.Distance(transform.position, targets[currentTarget].position) < 0.3f)
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
            agent.SetDestination(_player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetBool(_isWalkingHash, false);
            _anim.SetBool(_isAttackingHash, true);

        }
    }

    public void Attack(GameObject obj)
    {
        Vector3 targetPosition = new Vector3(transform.position.x,
                                        transform.position.y + 0.2f,
                                        transform.position.z);
        _player.GetComponent<PlayerV2>().TakeDamage(_damageCost);

        Instantiate(obj, targetPosition, Quaternion.LookRotation(transform.forward * -1, Vector3.up));
        GetComponent<AudioSource>().Play();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        rbody.materials = new Material[] { red };

        Invoke("ResetColor", 0.1f);

        if (_health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    void ResetColor()
    {
        rbody.materials = new Material[] { originalMaterial };
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
