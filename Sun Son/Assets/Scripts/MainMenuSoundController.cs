using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuSoundController : MonoBehaviour
{

    private Button _play;
    private Button _options;
    private Button _quit;
    private Button _back;
    private AudioSource _playSoundFx;
    private AudioSource _menuSoundFx;
    private GameObject _fade;

    void Start()
    {
        _play = GameObject.Find("/Canvas/MainMenu/PlayButton").GetComponent<Button>();
        _options = GameObject.Find("/Canvas/MainMenu/OptionsButton").GetComponent<Button>();
        _options.onClick.AddListener(playMenuSound);
        _quit = GameObject.Find("/Canvas/MainMenu/QuitButton").GetComponent<Button>();
        _quit.onClick.AddListener(playMenuSound);
        _fade = GameObject.Find("/Canvas/MainMenu/FadeToBlack");

        _playSoundFx = _play.GetComponent<AudioSource>();
        _menuSoundFx = GetComponent<AudioSource>();

    }

    public void playStartSound() {
        _fade.SetActive(true);
        _playSoundFx.Play();
    }

    public void playMenuSound() {
        _menuSoundFx.Play();
    }
}
