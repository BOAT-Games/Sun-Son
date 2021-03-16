using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaghetti : MonoBehaviour
{
   [SerializeField] Material _sky;
   [SerializeField] SceneFader _fader;

   public void Spaghetti2() {
      RenderSettings.skybox = _sky;
   }

   public void FadeOut() {
      _fader.normalFade();
      _fader.FadeToLevel("MainMenu");
   }
}
