using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuSoundController : MonoBehaviour
{
    private GameObject _play;
    private GameObject _options;
    private GameObject _quit;
    void Start()
    {
        _play = GameObject.Find("/Canvas/MainMenu/PlayButton");
        _options = GameObject.Find("/Canvas/MainMenu/OptionsButton");
        _quit = GameObject.Find("/Canvas/MainMenu/QuitButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
