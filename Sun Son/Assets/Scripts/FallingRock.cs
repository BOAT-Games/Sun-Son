using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] PlayerResources _player;
    [SerializeField] AudioSource _audio;
    public bool fall = false;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        _player = FindObjectOfType<PlayerResources>();
        targetPosition = new Vector3(transform.position.x, transform.position.y - 13.2f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (fall)
        {
            if (!_audio.isPlaying)
            {
                _audio.Play();
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, .5f);

            if (transform.position == targetPosition)
            {
                fall = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.TakeDamage(200);
        }
    }
}
