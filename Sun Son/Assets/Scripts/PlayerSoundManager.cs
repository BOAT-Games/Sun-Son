using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _movementAudio;
    [SerializeField] AudioSource _attackAudio;
    [SerializeField] AudioClip[] _footstep;
    [SerializeField] AudioClip _dash;
    [SerializeField] AudioClip _meleeAttack;

    void playFootstep()
    {
        if(!_movementAudio.isPlaying)
        {
            _movementAudio.clip = _footstep[Random.Range(0, _footstep.Length - 1)];
        }

        if (_movementAudio.clip != _dash)
        {
            _movementAudio.clip = _footstep[Random.Range(0, _footstep.Length - 1)];
            _movementAudio.Play();
        }
    }

    void playDash()
    {
        if(_movementAudio.clip != _dash)
        {
            _movementAudio.clip = _dash;
            _movementAudio.Play();
        }
    }
    
    void playMeleeAttack()
    {
        if(!_attackAudio.isPlaying)
        {
            _attackAudio.clip = _meleeAttack;
            _attackAudio.Play();
        }
    }

}
