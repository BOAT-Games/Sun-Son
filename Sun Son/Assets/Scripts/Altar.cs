using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField] Canvas _skillWindow;

    private void Update() {
        if (!Interactable._isInRange) {
            setInactiveSkillWindow();
        }
    }
    public void setActiveSkillWindow() {
        FindObjectOfType<PlayerV2>().lockControls(9999999f);
        _skillWindow.gameObject.SetActive(true);
    }

    public void setInactiveSkillWindow() {
        FindObjectOfType<PlayerV2>().unlockControls();
        _skillWindow.gameObject.SetActive(false);
    }
}
