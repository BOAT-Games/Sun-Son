using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCameraMvmt : MonoBehaviour {

   public GameObject player;

   void Update() {
      Vector3 camPos = gameObject.transform.position;
      if (player.transform.position.x >= camPos.x + 7.5) {
         gameObject.transform.position = camPos + new Vector3(14f, 0, 0);
      }
      else if (player.transform.position.x <= camPos.x - 7.5) {
         gameObject.transform.position = camPos - new Vector3(14f, 0, 0);
      }
   }
}
