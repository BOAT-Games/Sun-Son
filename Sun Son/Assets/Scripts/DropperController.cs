using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperController : MonoBehaviour
{
    [SerializeField] bool _drop = false;
    [SerializeField] float _dropTimer = 2f;
    [SerializeField] GameObject ps;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_drop && _dropTimer <= 0)
            {
                _drop = true;
            }
            else if (!_drop && _dropTimer > 0)
            {
                _dropTimer -= Time.deltaTime;
            }
            else if (_drop)
            {
                transform.position = new Vector3(transform.position.x,
                                            transform.position.y - 0.3f,
                                            transform.position.z);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Vector3 targetPosition = new Vector3(transform.position.x,
                                       transform.position.y - 2.5f, transform.position.z);
            Instantiate(ps, targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
