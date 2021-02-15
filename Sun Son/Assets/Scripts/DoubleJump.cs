using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [SerializeField] PlayerV2 _player;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            _player.setCanDoubleJump(true);
            Destroy(gameObject);
        }
    }
}
