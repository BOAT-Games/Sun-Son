using UnityEngine;

public class ProgressionBarrier : MonoBehaviour
{
   private GameObject barrierGO;

   [SerializeField] TMPro.TextMeshProUGUI remainingText;

   [SerializeField] GameObject[] collectibles;
   public int totalCollectibles;
   public int collectiblesRemaining;

   private void Start() {
      totalCollectibles = collectibles.Length;
      collectiblesRemaining = totalCollectibles;
      barrierGO = GameObject.FindGameObjectWithTag("Barrier");
      remainingText.text = totalCollectibles.ToString();
   }

   private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player" && collectiblesRemaining == 0) {
         Destroy(barrierGO);
         Destroy(gameObject);
      }
   }

   public void DecrementRemaining() {
      collectiblesRemaining -= 1;
      remainingText.text = collectiblesRemaining.ToString();
   }
}
