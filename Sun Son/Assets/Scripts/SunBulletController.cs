using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBulletController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name != "SunCharacterV2" &&
            !other.gameObject.name.Contains("DroppingEnemy") &&
            !other.gameObject.name.Contains("LightSource") &&
            !other.gameObject.CompareTag("Light"))
            Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name != "SunCharacterV2")
            if(!c.gameObject.CompareTag("Light"))
                Destroy(this.gameObject);
    }

}
