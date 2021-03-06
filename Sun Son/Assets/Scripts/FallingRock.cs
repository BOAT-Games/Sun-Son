using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] PlayerResources _player;
    public bool fall = false;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerResources>();
        targetPosition = new Vector3(transform.position.x, transform.position.y - 13.2f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (fall)
        { 
            transform.position = Vector3.Lerp(transform.position, targetPosition, .5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage(200);
        }
    }
}
