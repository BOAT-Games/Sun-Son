using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //code from Haungs
    public Camera cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
        //transform.LookAt(cam.transform);  // This does not look good for this example
        //transform.Rotate(0, 180, 0);

    }
}
