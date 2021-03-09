using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
    [SerializeField] SceneFader _fader;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneToLoad.Equals("Village2"))
            {
                _fader.specialFade();
            }
            else
            {
                _fader.normalFade();
            }
            _fader.FadeToLevel(SceneToLoad);
        }
    }
}
