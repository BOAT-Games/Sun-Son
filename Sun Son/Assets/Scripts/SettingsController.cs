using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    private void Start() {
        AudioMixer mixer = FindObjectOfType<AudioMixer>();
    }
    public void SetVolume(float volume) {
        mixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }
}
