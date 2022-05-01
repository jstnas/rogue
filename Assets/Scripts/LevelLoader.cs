using System.Collections;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance; // singleton

    private void Awake()
    {
        // destroy if clone
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // make singleton
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene().path;
        _instance.StartCoroutine(LoadSceneAsync(scene));
    }

    public static void LoadLevel(string scene)
    {
        _instance.StartCoroutine(LoadSceneAsync(scene));
    }

    private static IEnumerator LoadSceneAsync(string scene)
    {
        // display loading icon
        Loading.Load();
        var operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            print(operation.progress);
            yield return null;
        }
    }
}