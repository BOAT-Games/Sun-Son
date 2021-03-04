using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestructableGround : MonoBehaviour
{
   [SerializeField] List<GameObject> _groundPieces;
   private double basePieceLife = 2.5;

   private System.Random rand = new System.Random();

   void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player") {
         foreach (GameObject piece in _groundPieces) {
            Rigidbody pieceRB = piece.GetComponent<Rigidbody>();
            pieceRB.useGravity = true;
            pieceRB.velocity = new Vector3(0, (float)(-1.0 * rand.NextDouble()), 0);
            pieceRB.rotation = UnityEngine.Random.rotation;
            Destroy(piece, (float)(basePieceLife + rand.NextDouble()));
         }

         Destroy(gameObject, 0);
      }
   }
}
