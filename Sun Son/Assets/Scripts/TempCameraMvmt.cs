using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraMvmt : MonoBehaviour {

   public GameObject player;
   private static float camBaseHeight = 2.0f;

   void Update() {
      Vector3 camPos = gameObject.transform.position;
      if (player.transform.position.x >= camPos.x + 7.5) {
         gameObject.transform.position = camPos + new Vector3(14f, 0, 0);
      }
      else if (player.transform.position.x <= camPos.x - 7.5) {
         gameObject.transform.position = camPos - new Vector3(14f, 0, 0);
      }

      camPos = gameObject.transform.position;
      if (player.transform.position.y >= -1.0f) {
         gameObject.transform.position = new Vector3(camPos.x, camBaseHeight, camPos.z);
      }
      else if (player.transform.position.y < -1.0f) {
         gameObject.transform.position = new Vector3(camPos.x, -camBaseHeight, camPos.z);
      }
   }
}
