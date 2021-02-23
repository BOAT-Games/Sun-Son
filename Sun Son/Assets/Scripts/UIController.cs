using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIController : MonoBehaviour
{
    private PlayerResources _pr;
    private PlayerV2 _player;
    private PowerupController _powerups;
    private LightBar _lightBar;
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
        _lightBar.SetLightPoints(_pr.getCurrentLightPoints());
        _powerups.activateDashCooldown(_player.getIsDashCooldown());
        _powerups.activateDoubleJumpCooldown(_player.getHasDoubleJumped());
        _powerups.showDoubleJump(_player.getCanDoubleJump());
    }
}
