using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SunCutsceneMover : MonoBehaviour
{
    [SerializeField] SceneFader _fader;
    public void CutsceneEnd()
    {
        _fader.normalFade();
        _fader.FadeToLevel("CavesBoss");
    }
}
