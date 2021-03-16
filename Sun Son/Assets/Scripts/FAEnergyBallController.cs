using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FAEnergyBallController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            SceneManager.LoadScene("End Cutscene");
            Destroy(gameObject);
        }
    }
}
