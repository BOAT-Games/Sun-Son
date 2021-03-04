using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocksController : MonoBehaviour
{
    public float _timer = 4;
    private int curChild = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (_timer <= 0)
        {
            if (curChild < transform.childCount)
            {
                FallingRock child = transform.GetChild(curChild).gameObject.GetComponent<FallingRock>();

                child.fall = true;
                curChild++;
            }
            _timer = 1f;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
