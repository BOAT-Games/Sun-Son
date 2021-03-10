using UnityEngine;

public class ProgressionBarrier : MonoBehaviour
{
   public GameObject barrierGO;

   [SerializeField] TMPro.TextMeshProUGUI remainingText;

   [SerializeField] GameObject[] collectibles;
   public int totalCollectibles;
   public int collectiblesRemaining;

   private void Start() {
      totalCollectibles = collectibles.Length;
      collectiblesRemaining = totalCollectibles;
      barrierGO = GameObject.FindGameObjectWithTag("Barrier");
        if (remainingText != null)
        {
            remainingText.text = totalCollectibles.ToString();
        }
   }

    private void Update()
    {
        if (barrierGO == null)
        {
            barrierGO = GameObject.FindGameObjectWithTag("Barrier");
        }
    }

    private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player" && collectiblesRemaining == 0) {
         Destroy(barrierGO);
         Destroy(gameObject);
      }
   }

   public void DecrementRemaining() {
      collectiblesRemaining -= 1;
        if (remainingText != null)
        {
            remainingText.text = collectiblesRemaining.ToString();
        }
   }
}
