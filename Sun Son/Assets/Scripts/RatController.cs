using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatController : MonoBehaviour
{
    [SerializeField] int _damageCost = 2;
    [SerializeField] float _targetRange = 5;
    [SerializeField] float _speed = 0.02f;


    private GameObject _player;

    private bool goingLeft = false;
    private bool lockedOn = false;
    private float walkTimer = 2.0f;
    private float damageTimer = 1.0f;
    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = GetInRadius(_targetRange);

        if (walkTimer <= 0)
        {
            walkTimer = 2.0f;
            goingLeft = !goingLeft;
        }
        else
        {
            walkTimer -= Time.deltaTime;

            if (goingLeft && !lockedOn)
            {
                transform.position -= new Vector3(_speed, 0, 0);
            }
            else if (lockedOn)
            {
                targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed);
            }
            else
            {
                transform.position += new Vector3(_speed, 0, 0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "SunCharacterV2")
        {
            if (damageTimer <= 0)
            {
                _player.GetComponent<PlayerV2>().TakeDamage(_damageCost);
                damageTimer = 1.0f;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }

    Vector3 GetInRadius(float radius)
    {
        Collider[] array = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in array)
        {
            if (col.gameObject.name == "SunCharacterV2")
            { 
                lockedOn = true;
                return col.transform.position;
            }
        }
        lockedOn = false;
        return transform.position;
    }
}
