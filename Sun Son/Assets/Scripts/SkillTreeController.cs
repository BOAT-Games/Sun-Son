using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour
{
    [SerializeField] PlayerV2 _player;
    [SerializeField] Image _dashIcon;
    [SerializeField] Image _doubleJumpIcon;
    [SerializeField] Image _swordBranch1;
    [SerializeField] Image _gunBranch1;
    private bool _swordSkill = false;
    private bool _gunSkill = false;
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
        if (_player.getSkillPoints() > 0 & !_gunSkill) {
            _player.setSkillPoints(_player.getSkillPoints() - 1);
            _swordSkill = true;
            _swordBranch1.gameObject.SetActive(true);
        }
    }
        public void enableGun() {
        if (_player.getSkillPoints() > 0 & !_swordSkill) {
            _player.setSkillPoints(_player.getSkillPoints() - 1);
            _gunSkill = true;
            _gunBranch1.gameObject.SetActive(true);
        }
    }
}
