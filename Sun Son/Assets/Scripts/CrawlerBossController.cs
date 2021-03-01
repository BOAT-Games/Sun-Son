using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrawlerBossController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] int _health = 100;
    [SerializeField] int _damageCost = 40;

    //taking damage
    public SkinnedMeshRenderer rbody;
    public Material red;
    private Material originalMaterial;

    //death particles
    public GameObject ps;

    //movement targets
    [SerializeField] Transform[] targets;
    public int currentTarget = 0;
    NavMeshAgent _agent;

    //animations
    private Animator _anim;
    private int _isWalkingHash;
    private int _isAttackingHash;
    private int _isSummoningHash;


    private float timer = 3f;

    //booleans
    public bool paused = true;
    public bool attacked = false;
    public bool charged = false;
    public bool inRange = false;
    public bool stage1 = true;
    public bool stage2 = false;
    public bool stage3 = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerV2>().gameObject;
        originalMaterial = rbody.material;

        _anim = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isAttackingHash = Animator.StringToHash("IsAttacking");
        _isSummoningHash = Animator.StringToHash("IsSummoning");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pause between stages
        if (timer <= 0)
        {
            paused = false;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (_health > 66)
        {
            stage1 = true;
        }
        else if (_health > 33)
        {
            stage2 = true;
        }
        else
        {
            stage3 = true;
        }

        if (!paused && stage1 && !stage2 && !stage3)
        {
            //stage 1
            //charge to target 1
            if (!charged && !attacked)
            {
                Charge();
                charged = true;
            }
            //attack
            else if (!attacked && charged && currentTarget == 1 &&
                Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
            {
                _anim.SetBool(_isWalkingHash, false);
                _anim.SetBool(_isAttackingHash, true);

                attacked = true;
                paused = true;
                timer = 1;
            }
            //walk back to target 0
            else if (attacked && charged)
            {
                if (currentTarget == 0 &&
                    Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
                {

                    attacked = false;
                    charged = false;
                }
                else
                {
                    Retreat();
                }
            }
        }
        else if (!paused && stage1 && stage2 && !stage3)
        {
            //just switched to stage 2
            if (currentTarget == 0 &&
                    Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
            {

                stage1 = false;
            }
            else
            {
                Retreat();
            }
        }
        else if (!paused && !stage1 && stage2 && !stage3)
        {
            //stage 2
            //summon smaller crawlers
            //once all crawlers dead
            //follow stage 1 movements
        }
        else if (!paused && !stage1 && !stage2 && stage3)
        {
            //stage 3
            //retreat to back cave
            //big rock falls
            //wait on drop rock
            //take no damage anymore
        }
        
    }

    void Charge()
    {
        _anim.SetBool(_isWalkingHash, true);

        currentTarget = 1;
        _agent.speed = 30;

        _agent.SetDestination(targets[currentTarget].position);
    }

    void Retreat()
    {
        _anim.SetBool(_isWalkingHash, true);
        _anim.SetBool(_isAttackingHash, false);

        currentTarget = 0;
        _agent.speed = 5;

        _agent.SetDestination(targets[currentTarget].position);

    }

    public void Attack(GameObject obj)
    {
        Vector3 targetPosition = new Vector3(transform.position.x,
                                        transform.position.y + 0.2f,
                                        transform.position.z);

        if (inRange)
        {
            _player.GetComponent<PlayerV2>().TakeDamage(_damageCost);
            Instantiate(obj, targetPosition, Quaternion.LookRotation(transform.forward * -1, Vector3.up));
            GetComponent<AudioSource>().Play();
        }
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
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
