using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator fade;
    public AudioSource bgm;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(WaitForTransition(sceneName));
    }

    IEnumerator WaitForTransition(string sceneName)
    {
        fade.SetTrigger("FadeToBlack");
        StartCoroutine(AudioController.FadeOut(bgm, 2f));
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}
