using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
