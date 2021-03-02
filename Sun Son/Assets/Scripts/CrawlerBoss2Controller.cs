using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBoss2Controller : MonoBehaviour
{
    [SerializeField] GameObject _player;

    private GameObject currDrop;

    private void Start()
    {
        _player = FindObjectOfType<PlayerV2>().gameObject;
    }

    void Summon(GameObject obj)
    {
        //instantiate dropper
        if (GameObject.Find("CrawlerBoss") == null)
        {
            if (currDrop != null)
            {
                Destroy(currDrop);
            }
            currDrop = Instantiate(obj, new Vector3(_player.transform.position.x, transform.position.y + 5, _player.transform.position.z),
                Quaternion.identity);
        }
    }
}
