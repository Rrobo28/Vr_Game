using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class SceneLoader : MonoBehaviour
{
    public UnityEvent OnLoadBegin = new UnityEvent();
    public UnityEvent OnLoadEnd = new UnityEvent();

    private bool isLoading = false;

    public Fader fader;

    private void Awake()
    {
        SceneManager.sceneLoaded += SetActiveScene;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetActiveScene;
    }


    private void SetActiveScene(Scene scene,LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }

    public void LoadNewScene(string sceneName)
    {
        if (!isLoading)
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        isLoading = true;
        OnLoadBegin?.Invoke();
        while (!fader.StartFadeOut())
        {
            yield return null;  
        }
      
        yield return UnloadCurrent();
        yield return new WaitForSeconds(3);
        yield return StartCoroutine(LoadNew(sceneName));

        //Fade Out
        OnLoadEnd?.Invoke();
        while (!fader.StartFadeIn())
        {
            yield return null;
        }


            isLoading = false;
    }

    IEnumerator UnloadCurrent()
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

        while (!unloadOperation.isDone)
        {
            yield return null;
        }
       

        
    }
    IEnumerator LoadNew(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loadOperation.isDone)
        {
            yield return null;
        }
    }
}
