using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _movementAudio;
    [SerializeField] AudioSource _attackAudio;
    [SerializeField] AudioSource _shieldAudio;
    [SerializeField] AudioClip[] _footstep;
    [SerializeField] AudioClip _dash;
    [SerializeField] AudioClip _meleeAttack;
    [SerializeField] AudioClip _rangedAttack;
    [SerializeField] AudioClip _shield;

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

    void playRangedAttack()
    {
        _attackAudio.clip = _rangedAttack;
        _attackAudio.Play();
    }

    public void playShield()
    {
        if(!_shieldAudio.isPlaying)
        {
            _shieldAudio.clip = _shield;
            StartCoroutine(fadeInShield());
        }
    }

    private IEnumerator fadeInShield()
    {
        float currentTime = 0;
        float start = _shieldAudio.volume;

        _shieldAudio.Play();
        while (currentTime < 1f)
        {
            currentTime += Time.deltaTime;
            _shieldAudio.volume = Mathf.Lerp(start, 1, currentTime / 1f);
            yield return null;
        }
        yield break;
    }

    private IEnumerator fadeOutShield()
    {
        float currentTime = 0;
        float start = _shieldAudio.volume;

        while (currentTime < 0.5f)
        {
            currentTime += Time.deltaTime;
            _shieldAudio.volume = Mathf.Lerp(start, 0, currentTime / 0.5f);
            yield return null;
        }

        _shieldAudio.Stop();
        yield break;
    }

    public void stopShield()
    {
        if(_shieldAudio.isPlaying)
        {
            StartCoroutine(fadeOutShield());
        }
    }

}
