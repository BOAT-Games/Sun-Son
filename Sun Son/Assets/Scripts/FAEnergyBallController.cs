using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAEnergyBallController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            // Trigger end cutscene here

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
