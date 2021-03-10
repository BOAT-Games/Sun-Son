using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperController : MonoBehaviour
{
    [SerializeField] bool _drop = false;
    [SerializeField] bool _timerStart = false;
    [SerializeField] float _dropTimer = 2f;
    [SerializeField] GameObject ps;

    [SerializeField] Light leftEye;
    [SerializeField] Light rightEye;

    // Start is called before the first frame update
    void Start()
    {
        leftEye.intensity = 0;
        rightEye.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (_timerStart)
        {
            _dropTimer -= Time.deltaTime;
            leftEye.intensity = 2;
            rightEye.intensity = 2;
            
        }

        if (!_drop && _dropTimer <= 0)
        {
            _drop = true;
        }

        if (_drop)
        {
            transform.position = new Vector3(transform.position.x,
                                        transform.position.y - 0.4f,
                                        transform.position.z);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!_timerStart && other.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            _timerStart = true;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && _drop)
        {
            Vector3 targetPosition = new Vector3(transform.position.x,
                                       transform.position.y - 2.5f, transform.position.z);
            Instantiate(ps, targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 targetPosition = new Vector3(transform.position.x,
                                       transform.position.y - 3.5f, transform.position.z);
            Instantiate(ps, targetPosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
