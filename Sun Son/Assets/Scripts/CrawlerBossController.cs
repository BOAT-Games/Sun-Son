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

    //player attacks
    private PlayerShield _shield;

    //death particles
    public GameObject ps;

    //movement targets
    [SerializeField] Transform[] targets;
    private int currentTarget = 0;
    NavMeshAgent _agent;

    //animations
    private Animator _anim;
    private int _isWalkingHash;
    private int _isAttackingHash;
    private int _isSummoningHash;


    private float timer = 3f;

    //event objects
    public Transform rock;
    private GameObject wrapper;
    private int count = 0;
    private GameObject door;
    private bool hasPlayed = false;

    public AudioClip hiss;
    public AudioClip roar;

    //booleans
    private bool paused = true;
    private bool attacked = false;
    private bool charged = false;
    private bool stomped = false;
    private bool inRange = false;
    public bool stage1 = true;
    public bool stage2 = false;
    public bool stage3 = false;

    private void Awake()
    {
        wrapper = GameObject.Find("Level2Boss");
        door = GameObject.Find("Door");
        rock = GameObject.Find("Big Rock").transform;


        _agent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<PlayerResources>().gameObject;

        _shield = _player.GetComponent<PlayerShield>();

        originalMaterial = rbody.material;

        _anim = GetComponent<Animator>();

        _isWalkingHash = Animator.StringToHash("IsWalking");
        _isAttackingHash = Animator.StringToHash("IsAttacking");
        _isSummoningHash = Animator.StringToHash("IsSummoning");
    }
    // Start is called before the first frame update
    void Start()
    {
        stage1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //start once player is in arena
        if (_player.transform.position.x > -16)
        {
            //boss activates, player is trapped
            door.transform.localScale = Vector3.Slerp(door.transform.localScale, new Vector3(10, 5, 15),
                                        0.2f);

            if (!hasPlayed)
            {
                hasPlayed = true;
                wrapper.GetComponent<AudioSource>().Play();
                door.GetComponent<ParticleSystem>().Play();
            }

            //timer for pausing between actions
            if (timer <= 0)
            {
                paused = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        //update atack point to always be player
        targets[1].position = new Vector3(_player.transform.position.x + 5, targets[1].transform.position.y,
                                targets[1].transform.position.z);


        //health to measue what stage
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

        //actions
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
            else if (!attacked && charged)
            {
                if (currentTarget == 1 &&
                Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
                { 
                    _anim.SetBool(_isWalkingHash, false);
                    _anim.SetBool(_isAttackingHash, true);

                    attacked = true;
                    paused = true;
                    timer = 1;
                }
                else
                {
                    _agent.SetDestination(targets[currentTarget].position);
                }

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
            //just switched to stage to 2
            stage1 = false;
            stomped = true;
            charged = true;
            attacked = true;

        }
        else if (!paused && !stage1 && stage2 && !stage3)
        {
            //stage 2 summon and charge
            if (!stomped && !charged && !attacked)
            {
                Stomp();
                stomped = true;

                paused = true;
                timer = 1;
            }
            else if (stomped && !charged && !attacked)
            {
                _anim.SetBool(_isSummoningHash, false);
                Charge();
                charged = true;
            }
            //attack
            else if (stomped && !attacked && charged)
            {
                if (currentTarget == 1 &&
                Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
                {
                    _anim.SetBool(_isWalkingHash, false);
                    _anim.SetBool(_isAttackingHash, true);

                    attacked = true;
                    paused = true;
                    timer = 1;
                }
                else
                {
                    _agent.SetDestination(targets[currentTarget].position);
                }
            }
            //walk back to target 0
            else if (stomped && attacked && charged)
            {
                if (currentTarget == 0 &&
                    Vector3.Distance(transform.position, targets[currentTarget].position) < 1)
                {
                    ResetPosition();

                    attacked = false;
                    charged = false;
                    stomped = false;
                }
                else
                {
                    Retreat();
                }
            }
        }
        else if (!paused && !stage1 && stage2 && stage3)
        {
            //just switched to stage 3
            //escape to cave
            _anim.SetBool(_isWalkingHash, true);
            _anim.SetBool(_isAttackingHash, false);
            _anim.SetBool(_isSummoningHash, false);

            currentTarget = 2;
            _agent.speed = 30;

            _agent.SetDestination(targets[currentTarget].position);
            stage2 = false;
        }
        else if (!paused && !stage1 && !stage2 && stage3)
        {
            //roar as run away
            if (!wrapper.GetComponent<AudioSource>().isPlaying && count == 0)
            {
                count++;
                wrapper.GetComponent<AudioSource>().clip = roar;
                wrapper.GetComponent<AudioSource>().Play();
            }
            //big rock falls and opens up upper area
            if (Vector3.Distance(transform.position, targets[2].position) < 5)
            {
                _anim.SetBool(_isWalkingHash, false);
                _anim.SetBool(_isAttackingHash, false);
                _anim.SetBool(_isSummoningHash, false);
                Destroy(gameObject);
            }
            //spider passes under rock
            else if (Vector3.Distance(transform.position, targets[2].position) < 20)
            {
                //drop the rock
                if (!rock.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    rock.gameObject.GetComponent<AudioSource>().Play();
                }
                Vector3 targetPos = new Vector3(targets[0].position.x, targets[0].position.y + 1,
                                        targets[0].position.z);
                float step = 20 * Time.deltaTime;
                rock.position = Vector3.MoveTowards(rock.position, targetPos, step);

            }
            else
            {
                //little shake to signal fall
                rock.position = new Vector3(rock.position.x + Mathf.Sin(Time.time * 20) * 0.05f,
                                    rock.position.y + Mathf.Sin(Time.time * 20) * 0.1f,
                                    rock.position.z) ;
            }
        }

    }

    void ResetPosition()
    {
        _anim.SetBool(_isWalkingHash, false);
        currentTarget = 1;
        _agent.speed = 1;

        _agent.SetDestination(targets[currentTarget].position);

        paused = true;
        timer = 2;
    }

    void Stomp()
    {
        _anim.SetBool(_isSummoningHash, true);
        //stop moving
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    void Summon(GameObject obj)
    { 
        //instantiate dropper
        Instantiate(obj, new Vector3(_player.transform.position.x, _player.transform.position.y + 8, _player.transform.position.z),
            Quaternion.identity);
    }

    void Charge()
    {
        if (!wrapper.GetComponent<AudioSource>().isPlaying)
        {
            wrapper.GetComponent<AudioSource>().clip = hiss;
            wrapper.GetComponent<AudioSource>().Play();
        }
        _anim.SetBool(_isWalkingHash, true);

        currentTarget = 1;
        _agent.speed = 25;

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

        if (inRange && !_shield._shieldPressed)
        {
            _player.GetComponent<PlayerResources>().TakeDamage(_damageCost);
            Instantiate(obj, targetPosition, Quaternion.LookRotation(transform.forward * -1, Vector3.up));
        }
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
