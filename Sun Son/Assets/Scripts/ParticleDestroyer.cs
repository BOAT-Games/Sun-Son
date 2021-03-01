using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] GameObject _player;
    [SerializeField] int _damageCost = 20;
    private bool _called = false;


    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        _player = FindObjectOfType<PlayerV2>().gameObject;
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!_called && other.CompareTag("Player"))
        {
            _called = true;

            if (this.CompareTag("Shadow"))
            {
                _player.GetComponent<PlayerResources>().TakeDamage(_damageCost);
            }
            else
            {
                //add code to give player back some health
            }
        }
    }
}
