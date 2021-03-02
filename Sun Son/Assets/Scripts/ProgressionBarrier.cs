using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBarrier : MonoBehaviour
{
   private GameObject barrierGO;
   private ProgressionBarrier barrier;

   [SerializeField] GameObject[] collectibles;
   public int totalCollectibles;
   public int collectiblesRemaining;

   private void Start() {
      totalCollectibles = collectibles.Length;
      collectiblesRemaining = totalCollectibles;
      barrierGO = GameObject.FindGameObjectWithTag("Barrier");
      barrier = barrierGO.GetComponent<ProgressionBarrier>();
   }

   private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player" && collectiblesRemaining == 0) {
         Destroy(barrierGO);
         Destroy(gameObject);
      }
   }

   public void DecrementRemaining() {
      collectiblesRemaining -= 1;
   }
}
