using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private PlayerV2 _player;

    void Start() {
        _player = FindObjectOfType<PlayerV2>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerPrefs.SetInt("shield", 1);
            _player.GetComponent<PlayerShield>().enabled = true;
            Destroy(gameObject);
        }
    }
}
