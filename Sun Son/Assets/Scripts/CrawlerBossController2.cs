using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerBossController2 : MonoBehaviour
{
    [SerializeField] GameObject _player;

    //animations
    private Animator _anim;
    private int _isSummoningHash;

    public float timer = 3f;
    public bool paused = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerResources>().gameObject;

        _anim = GetComponent<Animator>();

        _isSummoningHash = Animator.StringToHash("IsSummoning");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.y > 107)
        {
            if (!paused)
            {
                Stomp();

                paused = true;
                timer = 5;
            }
            else
            {
                if (timer <= 0)
                {
                    paused = false;
                }
                else
                {
                    timer -= Time.deltaTime;
                    _anim.SetBool(_isSummoningHash, false);
                }
            }
        }
    }

    void Stomp()
    {
        _anim.SetBool(_isSummoningHash, true);
    }

    void Summon(GameObject obj)
    {
        //instantiate dropper
        Instantiate(obj, new Vector3(_player.transform.position.x, _player.transform.position.y + 8, _player.transform.position.z),
            Quaternion.identity);
    }
}
