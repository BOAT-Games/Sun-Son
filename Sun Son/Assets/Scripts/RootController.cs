using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{
    public DropRockController _controller;
    private AudioSource spider;

    // Start is called before the first frame update
    void Start()
    {
        spider = GameObject.Find("CrawlerBoss2").GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            spider.Play();
            _controller.rootCount--;
            Destroy(gameObject);
        }
    }
}
