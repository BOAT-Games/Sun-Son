using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunCutsceneMover : MonoBehaviour
{
    [SerializeField] SceneFader _fader;
    [SerializeField] string _sceneToLoad;
    public void CutsceneEnd()
    {
        _fader.normalFade();
        _fader.FadeToLevel(_sceneToLoad);
    }
}
