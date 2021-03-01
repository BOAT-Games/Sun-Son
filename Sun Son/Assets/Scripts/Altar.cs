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
        _skillWindow.gameObject.SetActive(true);
    }

    public void setInactiveSkillWindow() {
        _skillWindow.gameObject.SetActive(false);
    }
}
