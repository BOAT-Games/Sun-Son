using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalController : MonoBehaviour
{
    [SerializeField] int _health = 20;
    [SerializeField] float _timer = 2;
    [SerializeField] int _damage = 20;


    [SerializeField] bool _atPlayer = false;
    [SerializeField] bool _attacked = false;
    [SerializeField] bool _inRange = false;
    [SerializeField] bool _portalSet = false;

    private Animator _anim;
    private int _isAttackingHash;

    private GameObject _player;
    private Vector3 startPosition;
    [SerializeField] GameObject portal;
    [SerializeField] GameObject flash;
    private GameObject portalClone;

    public Material red;
    public Material originalMaterial;

    [SerializeField] GameObject ps;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
        startPosition = transform.position;

        _anim = GetComponent<Animator>();
        _isAttackingHash = Animator.StringToHash("IsAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        //always look at player...creppy
        transform.LookAt(_player.transform.position);

        if (_timer <= 0)
        {
            Vector3 targetPos = new Vector3(_player.transform.position.x - 1.2f,
                                                _player.transform.position.y,
                                                _player.transform.position.z);

            if (!_portalSet && !_atPlayer && !_attacked)
            {
                //set portal next to player
                Vector3 portalPos = new Vector3(targetPos.x, targetPos.y + 0.1f, targetPos.z);
                portalClone = Instantiate(portal, portalPos,
                                Quaternion.LookRotation(Vector3.up, Vector3.up));
                _portalSet = true;
                _timer = 2;
            }
            else if (_portalSet && !_atPlayer && !_attacked)
            {
                //teleport to player
                Vector3 flashPos = new Vector3(targetPos.x, targetPos.y + 1.3f, targetPos.z - 1);
                Instantiate(flash, flashPos, Quaternion.LookRotation(Vector3.up, Vector3.up));

                Destroy(portalClone);
                transform.position = targetPos;

                _atPlayer = true;
                _portalSet = false;
                _timer = 1;
            }
            else if (_atPlayer && !_attacked)
            {
                _anim.SetBool(_isAttackingHash, true);
                _attacked = true;
                _timer = 2;
            }
            else if (!_portalSet && _atPlayer && _attacked)
            {
                //set portal to be at original position
                Vector3 portalPos = new Vector3(startPosition.x, startPosition.y + 0.1f, startPosition.z);
                portalClone = Instantiate(portal, portalPos,
                                Quaternion.LookRotation(Vector3.up, Vector3.up));
                _portalSet = true;
                _timer = 2;
            }
            else if (_portalSet && _atPlayer && _attacked)
            {
                _anim.SetBool(_isAttackingHash, false);

                Vector3 flashPos = new Vector3(startPosition.x, startPosition.y + 1.3f, startPosition.z - 1);
                Instantiate(flash, flashPos, Quaternion.LookRotation(Vector3.up, Vector3.up));

                //teleport back to original position
                Destroy(portalClone);
                transform.position = startPosition;
                _atPlayer = false;
                _attacked = false;
                _portalSet = false;
                _timer = 5;
            }
        }
        else
        {
            _timer -= Time.deltaTime;
        }



    }

    void Attack()
    {
        if (_inRange)
        {
            _player.GetComponent<PlayerV2>().TakeDamage(_damage);
        }
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = false;
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        SetDamageColor();

        Invoke("ResetColor", 0.1f);

        if (_health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    void SetDamageColor()
    {
        Transform modelChild = transform.GetChild(1);

        foreach(Transform child in modelChild)
        {
            //for each material
            SkinnedMeshRenderer renderer = child.GetComponent<SkinnedMeshRenderer>();
            Material[] mats = renderer.materials;

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                mats[i] = red;
            }

            renderer.materials = mats;
        }
    }

    void ResetColor()
    {
        Transform modelChild = transform.GetChild(1);

        foreach (Transform child in modelChild)
        {
            //for each material
            SkinnedMeshRenderer renderer = child.GetComponent<SkinnedMeshRenderer>();
            Material[] mats = renderer.materials;

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                mats[i] = originalMaterial;
            }

            renderer.materials = mats;
        }
    }

    private void DestroyEnemy()
    {
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
