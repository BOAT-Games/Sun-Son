using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRockController : MonoBehaviour
{
    public int rootCount = 3;

    [SerializeField] Transform targetPos;
    [SerializeField] GameObject spider;
    [SerializeField] GameObject boss;
    [SerializeField] AudioClip roar;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("CrawlerBoss");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            if (rootCount == 0)
            {
                if (count == 0)
                {
                    GetComponent<AudioSource>().Play();
                    spider.GetComponent<AudioSource>().clip = roar;
                    spider.GetComponent<AudioSource>().Play();
                    count++;
                }
                float step = 40 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, step);
            }

            if (Vector3.Distance(transform.position, targetPos.position) < 5)
            {
                Destroy(spider);
            }
        }
    }

}
