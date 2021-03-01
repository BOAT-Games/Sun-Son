using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerupController : MonoBehaviour
{
    private PlayerV2 _player;

    // Fields related to dash
    [SerializeField] Image _dashCooldownImage;
    private float _dashCooldown;

    // Fields related to doublejump
    [SerializeField] Image _doubleJumpImage;
    [SerializeField] Image _doubleJumpCooldownImage;
    [SerializeField] TextMeshProUGUI _doubleJumpText;
    [SerializeField] DoubleJump _doubleJump;

    void Start() {
        
        _player = FindObjectOfType<PlayerV2>();

        _dashCooldownImage.fillAmount = 0;
        _doubleJumpImage.fillAmount = 0;
        _doubleJumpCooldownImage.fillAmount = 1;
        _doubleJumpText.enabled = false;

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

    // Unfortunately there's no real "cooldown" on just gravity since
    // Player might be at variable heights
    public void showDoubleJump(bool hasDoubleJump) {
        if (hasDoubleJump) {
            _doubleJumpImage.fillAmount = 1;
            _doubleJumpText.enabled = true;
        }
    }

    public void activateDoubleJumpCooldown(bool hasDoubleJumped) {

        if (hasDoubleJumped) {
            _doubleJumpCooldownImage.fillAmount = 1;
        }
        else {
            _doubleJumpCooldownImage.fillAmount = 0;
        }
    }


    public void setDashCooldown(float dashCooldown) {
        _dashCooldown = dashCooldown;
    }

}
