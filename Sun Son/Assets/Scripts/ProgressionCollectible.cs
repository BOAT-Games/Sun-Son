
using System;
using UnityEngine;

public class ProgressionCollectible : MonoBehaviour
{
   public ProgressionBarrier barrier;

   private void Start() {
       GameObject barrierGO = GameObject.FindGameObjectWithTag("Barrier Trigger");
       barrier = barrierGO.GetComponent<ProgressionBarrier>();
   }

    private void Update()
    {
       /* if (barrier == null)
        {
            FindNextBarrier();
        }*/
    }

   private void OnTriggerStay(Collider other) {
      if (other.tag == "Player") {
         /*if (barrier == null)
         {

             FindNextBarrier();
         }*/

         barrier.DecrementRemaining();
         Destroy(gameObject);
      }
   }

   private void FindNextBarrier()
    {
        GameObject barrierGO = GameObject.FindGameObjectWithTag("Barrier Trigger");
        barrier = barrierGO.GetComponent<ProgressionBarrier>();
    }
}
