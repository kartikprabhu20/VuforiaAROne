using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private string sceneNameToBeLoaded;

    public void LoadScene(string sceneName)
    {
        sceneNameToBeLoaded = sceneName;
        StartCoroutine(InitializeSceneLoading());
    }

    IEnumerator InitializeSceneLoading()
    {
        yield return SceneManager.LoadSceneAsync("Scene_Loading");
        StartCoroutine(LoadActualScene());
    }

    IEnumerator LoadActualScene()
    {
        var asyncSceneLoading = SceneManager.LoadSceneAsync(sceneNameToBeLoaded);
        asyncSceneLoading.allowSceneActivation = false;// scene is not displayed when it is loading

        while (!asyncSceneLoading.isDone)
        {
            Debug.Log(asyncSceneLoading.progress);
            if (asyncSceneLoading.progress >= 0.9f)
            {
                asyncSceneLoading.allowSceneActivation = true;//if loading is 90% then load the scnee
            }
            yield return null;
        }


    }
}
