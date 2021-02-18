using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TalkText : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.text = "Hello";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(string x)
    {
        _text.text = x;
    }
}
