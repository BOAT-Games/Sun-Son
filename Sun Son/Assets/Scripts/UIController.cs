using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIController : MonoBehaviour
{
    [SerializeField] PlayerV2 _player;
    [SerializeField] PowerupController _powerups;
    [SerializeField] LightBar _lightBar;
    private int _currentLightPoints;
    private int _maxLightPoints;
    void Start()
    {
        _maxLightPoints = _player.getMaxLightPoints();
        _currentLightPoints = _maxLightPoints;
        _lightBar.SetMaxLightPoints(_currentLightPoints);
        _lightBar.SetLightPoints(_currentLightPoints);
        _powerups.setDashCooldown(_player.getDashDelay());
    }

    // Update is called once per frame
    void Update()
    {
        _lightBar.SetLightPoints(_player.getCurrentLightPoints());
        _powerups.activateDashCooldown(_player.getIsDashCooldown());
        _powerups.activateDoubleJumpCooldown(_player.getHasDoubleJumped());
        _powerups.showDoubleJump(_player.getCanDoubleJump());
    }
}
