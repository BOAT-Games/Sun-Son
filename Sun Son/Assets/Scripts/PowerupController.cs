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
    private bool _dashDisabled;
    private bool _doubleJumpDisabled;

    void Start() {
        
        _player = FindObjectOfType<PlayerV2>();

        _dashCooldownImage.fillAmount = 0;
        _doubleJumpImage.fillAmount = 0;
        _doubleJumpCooldownImage.fillAmount = 1;
        _doubleJumpText.enabled = false;
        _dashDisabled = false;
        _doubleJumpDisabled = false;
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

    public void disableDashIcon(int lightPoints) {
        if (lightPoints <= 20)  {
            _dashCooldownImage.fillAmount = 1;
            _dashDisabled = true;
        }
        if (_dashDisabled == true & lightPoints > 20) {
            Debug.Log("MADE IT");
            _dashCooldownImage.fillAmount = 0;
            _dashDisabled = false;
        }
    } 

    public void disableDoubleJumpIcon(int lightPoints) {
        if (lightPoints <= 10 & _player.getHasDoubleJumpAbility()) { 
            _doubleJumpCooldownImage.fillAmount = 1;
            _doubleJumpDisabled = true;
        }
        if (_doubleJumpDisabled == true & lightPoints > 10) {
            _doubleJumpCooldownImage.fillAmount = 0;
            _doubleJumpDisabled = false;
        }
    } 
}
