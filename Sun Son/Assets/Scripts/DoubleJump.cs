using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    private PlayerV2 _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerV2>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player")) {
            PlayerPrefs.SetInt("doubleJump", 1);
            _player.setCanDoubleJump(true);
            _player.setHasDoubleJumpAbility(true);
            Destroy(gameObject);
        }
    }
}
