using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavesBossIntroScript : MonoBehaviour
{
    [SerializeField] GameObject _player;
    void Start()
    {
        _player.GetComponent<PlayerV2>().lockControls(99999999);
    }
}
