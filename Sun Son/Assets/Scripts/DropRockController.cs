using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRockController : MonoBehaviour
{
    public List<GameObject> roots;

    [SerializeField] Transform targetPos;
    [SerializeField] GameObject spider;
    [SerializeField] GameObject boss;

    private Animator _anim;
    private int _isSummoningHash;

    private float timer = 3f;
    private bool paused = true;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("CrawlerBoss");
        _anim = spider.GetComponent<Animator>();
        _isSummoningHash = Animator.StringToHash("IsSummoning");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            if (timer <= 0)
            {
                paused = false;
            }
            else
            {
                timer -= Time.deltaTime;
            }

            if (!paused)
            {
                Stomp();

                timer = 3f;
                paused = true;
            }
            else
            {
                _anim.SetBool(_isSummoningHash, false);
            }


            if (roots.Count == 0)
            {
                float step = 40 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, step);
            }

            if (Vector3.Distance(transform.position, targetPos.position) < 5)
            {
                Destroy(spider);
            }
        }
    }

    void Stomp()
    {
        _anim.SetBool(_isSummoningHash, true);
    }

}
