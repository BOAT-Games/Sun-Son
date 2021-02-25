using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] int _damageCost;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
        _damageCost = _player.GetComponent<PlayerResources>().getMaxLightPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.GetComponent<PlayerResources>().TakeDamage(_damageCost);
        }
    }
}
