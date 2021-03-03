using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBoss2Controller : MonoBehaviour
{
    [SerializeField] GameObject _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
    }

    void Summon(GameObject obj)
    {
        //instantiate dropper
        if (GameObject.Find("DroppingEnemy(Clone)") == null)
        {
            Instantiate(obj, new Vector3(_player.transform.position.x, transform.position.y + 5, _player.transform.position.z),
                Quaternion.identity);
        }
    }
}
