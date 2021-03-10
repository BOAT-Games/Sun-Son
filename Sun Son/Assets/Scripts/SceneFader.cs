using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Animator animator;
    private string _sceneToLoad;

    public void FadeToLevel(string sceneToLoad)
    {
        _sceneToLoad = sceneToLoad;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadedComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void specialFade()
    {
        gameObject.GetComponent<Image>().color = new Color32(255, 255, 153, 0);
    }

    public void normalFade()
    {
        gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
    }
}
