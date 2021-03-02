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

    [SerializeField] GameObject _gameOver;

    private int _currentLightPoints;
    private int _maxLightPoints;

    void Start()
    {
        _player = FindObjectOfType<PlayerV2>();
        _pr = FindObjectOfType<PlayerResources>();
        _powerups = FindObjectOfType<PowerupController>();
        _lightBar = FindObjectOfType<LightBar>();

        _maxLightPoints = _pr.getMaxLightPoints();
        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_currentLightPoints);
        _lightBar.SetLightPoints(_currentLightPoints);
        _powerups.setDashCooldown(_player.getDashDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameOver.activeSelf == false) Time.timeScale = 1;
        if (_player.getCurrentLightPoints() <= 0) {
            _gameOver.SetActive(true);
            Time.timeScale = 0;
        }
        _lightBar.SetLightPoints(_pr.getCurrentLightPoints());
        _powerups.activateDashCooldown(_player.getIsDashCooldown());
        _powerups.disableDashIcon(_player.getCurrentLightPoints());
        _powerups.activateDoubleJumpCooldown(_player.getHasDoubleJumped());
        _powerups.disableDoubleJumpIcon(_player.getCurrentLightPoints());
        _powerups.showDoubleJump(_player.getCanDoubleJump());
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() 
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
