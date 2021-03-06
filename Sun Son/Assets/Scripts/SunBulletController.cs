using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBulletController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("he");

        if (c.gameObject.name != "SunCharacterV2")
            if(!c.gameObject.CompareTag("Light"))
                Destroy(this.gameObject);
    }

}
