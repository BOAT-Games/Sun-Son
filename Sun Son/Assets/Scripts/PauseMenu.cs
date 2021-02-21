using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls _input;
    private bool _pausePressed = false;
    public static bool _isPaused = false;

    public GameObject pauseMenuUi;
    public void Awake() {
        _input = new PlayerControls();

        _input.CharacterControls.PauseGame.performed += ctx => _pausePressed = ctx.ReadValueAsButton();
 
    }
    private void OnEnable()
    {
        _input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        _input.CharacterControls.Disable();
    }
    public void Update() {
        if (_pausePressed == true)
            HandlePause();
    }
    private void HandlePause() {
        if (_isPaused) Resume();
        else Pause();
    }
    public void Resume() {
        Debug.Log("Resuming the game");
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
        _pausePressed = false;
    }

    public void Pause() {
        Debug.Log("Pausing the game");
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
        _pausePressed = false;
    }

    public void Quit() {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
