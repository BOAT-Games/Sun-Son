using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip[] _footstep;
    [SerializeField] AudioClip _dash;

    void playFootstep()
    {
        if(!_audioSource.isPlaying)
        {
            _audioSource.clip = _footstep[Random.Range(0, _footstep.Length - 1)];
        }

        if (_audioSource.clip != _dash)
        {
            _audioSource.clip = _footstep[Random.Range(0, _footstep.Length - 1)];
            _audioSource.Play();
        }
    }

    void playDash()
    {
        if(_audioSource.clip != _dash)
        {
            _audioSource.clip = _dash;
            _audioSource.Play();
        }
    }


}
