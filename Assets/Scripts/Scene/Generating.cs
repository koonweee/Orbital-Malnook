using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generating : MonoBehaviour
{
    public SceneLoader loader;
    private AsyncOperation asyncLoad;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(1);

        asyncLoad = SceneManager.LoadSceneAsync("In Game");
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}

        //loader.LoadScene("In Game");
    }

    public void SwitchScene()
    {
        Debug.Log("SWAPPIN");
        asyncLoad.allowSceneActivation = true;
    }
}
