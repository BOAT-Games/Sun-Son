using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
    [SerializeField] SceneFader _fader;

   private void Start() {
      if (SceneToLoad.Equals("Tutorial_Level")) {
         PlayerPrefs.DeleteAll();
      }
   }

   private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (SceneToLoad.Equals("Village2"))
            {
                PlayerPrefs.SetInt("dash", 1);
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
