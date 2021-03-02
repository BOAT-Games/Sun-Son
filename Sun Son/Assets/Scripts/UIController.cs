using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    private PlayerResources _pr;
    private PlayerV2 _player;
    private PowerupController _powerups;
    private LightBar _lightBar;

    private GameObject _gameOver;

    private int _currentLightPoints;
    private int _maxLightPoints;

    void Start()
    {
        _player = FindObjectOfType<PlayerV2>();
        _pr = FindObjectOfType<PlayerResources>();
        _powerups = FindObjectOfType<PowerupController>();
        _gameOver = GameObject.Find("/Player_UI_Camera_Rig/Canvas/GameOver");
        _lightBar = FindObjectOfType<LightBar>();

        _maxLightPoints = _pr.getMaxLightPoints();
        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_currentLightPoints);
        _lightBar.SetLightPoints(_currentLightPoints);
        _powerups.setDashCooldown(_player.getDashDelay());
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pr.getCurrentLightPoints() <= 0) {
            _gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        _lightBar.SetLightPoints(_pr.getCurrentLightPoints());
        _powerups.activateDashCooldown(_player.getIsDashCooldown());
        _powerups.disableDashIcon(_pr.getCurrentLightPoints());
        _powerups.activateDoubleJumpCooldown(_player.getHasDoubleJumped());
        _powerups.disableDoubleJumpIcon(_pr.getCurrentLightPoints());
        _powerups.showDoubleJump(_player.getCanDoubleJump());
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame() 
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
