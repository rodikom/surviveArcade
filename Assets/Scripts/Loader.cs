using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private class LoadingMonoBehavior : MonoBehaviour { }
    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;
    public enum Scene
    {
        MainMenu,
        Loading,
        Game,
        Death
    }
    public static void Loading(Scene scene)
    {
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehavior>().StartCoroutine(LoadSceneAsync(scene));
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }
    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone) {
            yield return null;
        }
    }
    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null) {
            return loadingAsyncOperation.progress;
        } else {
            return 1f;
        }
    }
    public static void LoaderCallback()
    {
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
