using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
        originalMaterial = rbody.material;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
