
using System;
using UnityEngine;

public class ProgressionCollectible : MonoBehaviour
{
   private ProgressionBarrier barrier;

   private void Start() {
      GameObject barrierGO = GameObject.FindGameObjectWithTag("Barrier Trigger");
      barrier = barrierGO.GetComponent<ProgressionBarrier>();
   }

    private void Update()
    {
        if (barrier == null)
        {
            GameObject barrierGO = GameObject.FindGameObjectWithTag("Barrier Trigger");
            barrier = barrierGO.GetComponent<ProgressionBarrier>();
        }
    }
    private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player") {
         barrier.DecrementRemaining();
         Destroy(gameObject);
      }
   }
}
