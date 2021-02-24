using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowTextInRange : MonoBehaviour
{
   public TextMeshProUGUI text;

   private float fadeSpeed = 1.0f;
   private bool visible = false;
   private bool fading = false;

   private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player" && !visible && !fading) {
         StartCoroutine(FadeIn(text));
      }
   }

   private void OnTriggerExit(Collider other) {
      if (other.tag == "Player") {
         StartCoroutine(FadeOut(text));
      }
   }

   private IEnumerator FadeIn(TextMeshProUGUI text) {
      float red = text.color.r;
      float green = text.color.g;
      float blue = text.color.b;

      text.color = new Color(red, green, blue, 0);
      fading = true;
      while (text.color.a < 1.0f) {
         text.color = new Color(red, green, blue, text.color.a + (Time.deltaTime / fadeSpeed));
         yield return null;
      }
      fading = false;

      visible = true;
   }

   private IEnumerator FadeOut(TextMeshProUGUI text) {
      float red = text.color.r;
      float green = text.color.g;
      float blue = text.color.b;

      text.color = new Color(red, green, blue, 0);
      fading = true;
      while (text.color.a > 0.0f) {
         text.color = new Color(red, green, blue, text.color.a - (Time.deltaTime / fadeSpeed));
         yield return null;
      }
      fading = false;

      visible = false;
   }
}
