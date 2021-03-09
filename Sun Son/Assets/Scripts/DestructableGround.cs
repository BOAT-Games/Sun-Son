using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestructableGround : MonoBehaviour
{
   [SerializeField] List<GameObject> _groundPieces;
   [SerializeField] GameObject _ground;
   private double basePieceLife = 2.5;
   private AudioSource _audio;

   private System.Random rand = new System.Random();

private void Start() {
   _audio = _ground.GetComponent<AudioSource>();
}
   void OnTriggerEnter(Collider other) {

      if (other.gameObject.tag == "Player") {

         foreach (GameObject piece in _groundPieces) {
            _audio.Play();
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
