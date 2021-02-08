using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupController : MonoBehaviour
{
    [SerializeField] Image _dashCooldownImage;
    [SerializeField] Player _player;
    private float _dashCooldown;

    void Start() {
        _dashCooldownImage.fillAmount = 0;
    }
    public void activateDashCooldown(bool isCooldown) {
        if (isCooldown) {
             timeCooldown(_dashCooldown, _dashCooldownImage);
        }
    }

    public void timeCooldown(float cooldown, Image image) {
        image.fillAmount += 1 / cooldown * Time.deltaTime;
        if (image.fillAmount >= 1) {
            image.fillAmount = 0;
            _player.setIsDashCooldown(false);
        }
    }

    public void setDashCooldown(float dashCooldown) {
        _dashCooldown = dashCooldown;
    }
}
