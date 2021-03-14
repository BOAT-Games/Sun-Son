using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control_lock : MonoBehaviour
{
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        other.GetComponent<PlayerV2>().lockControls(99999999);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
