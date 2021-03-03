using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] int _damageCost = 2;

    private PlayerShield _shield;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
        _shield = _player.GetComponent<PlayerShield>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_shield._shieldPressed)
        {
            _player.GetComponent<PlayerResources>().TakeDamage(_damageCost);
        }
        Destroy(gameObject);
    }
}
