using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBossTarget : MonoBehaviour
{
    [SerializeField] CrawlerBossController _boss;

    // Start is called before the first frame update
    void Start()
    {
        _boss = GameObject.Find("CrawlerBoss").GetComponent<CrawlerBossController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _boss.TakeDamage(5);
        }
    }
}
