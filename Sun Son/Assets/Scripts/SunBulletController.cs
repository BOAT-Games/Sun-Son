using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBulletController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name != "SunCharacterV2")
            Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name != "SunCharacterV2")
            Destroy(this.gameObject);
    }

}
