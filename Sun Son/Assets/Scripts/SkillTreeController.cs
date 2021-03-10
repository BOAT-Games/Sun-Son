using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour
{
    private PlayerV2 _player;
    private PlayerResources _pr;
    [SerializeField] Image _dashIcon;
    [SerializeField] Image _doubleJumpIcon;
    [SerializeField] Image _swordBranch1;
    [SerializeField] Image _gunBranch1;
    private bool _swordSkill = false;
    private bool _gunSkill = false;

    private void Start() {
         _player = FindObjectOfType<PlayerV2>();
        _pr = FindObjectOfType<PlayerResources>();
    }
    private void Update() {
        enableDashIcon(_player.getHasDashAbility());
        enableDoubleJumpIcon(_player.getHasDoubleJumpAbility());
    }

    private void enableDashIcon(bool enable) {
        if (enable) {
            _dashIcon.gameObject.SetActive(true);
        }
    }

    private void enableDoubleJumpIcon(bool enable) {
        if (enable) {
            _doubleJumpIcon.gameObject.SetActive(true);
        }
    }

    public void enableSword() {
        if (_pr.getSkillPoints() > 0 & !_gunSkill) {
            _pr.setSkillPoints(_pr.getSkillPoints() - 1);
            PlayerPrefs.SetInt("melee", 1);
            PlayerPrefs.SetInt("ranged", 0);
            _player.GetComponent<PlayerMelee>().enabled = true;
            _player.GetComponent<PlayerRanged>().enabled = false;
            _swordSkill = true;
            _swordBranch1.gameObject.SetActive(true);
        }
    }
    public void enableGun() {
        if (_pr.getSkillPoints() > 0 & !_swordSkill) {
            _pr.setSkillPoints(_pr.getSkillPoints() - 1);
            PlayerPrefs.SetInt("melee", 0);
            PlayerPrefs.SetInt("ranged", 1);
            _player.GetComponent<PlayerMelee>().enabled = false;
            _player.GetComponent<PlayerRanged>().enabled = true;
            _gunSkill = true;
            _gunBranch1.gameObject.SetActive(true);
        }
    }
}
