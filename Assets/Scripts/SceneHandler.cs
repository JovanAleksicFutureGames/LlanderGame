using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;
    public int sceneCountTest;
    private DummyRocket _dummyRocket;
    private Fader _fader;
    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            _dummyRocket = GameObject.FindObjectOfType<DummyRocket>();
        }
        else 
            _dummyRocket = null;
        sceneCountTest = SceneManager.sceneCountInBuildSettings;
        _fader = GameObject.FindObjectOfType<Fader>();
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            MainMenu();
        }
        else 
        {
            SceneManager.LoadSceneAsync(GetNextSceneIndex());
        }
    }

    public int GetNextSceneIndex() 
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        return currentIndex + 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void StartGameMainMenu() 
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void RestartCurrentLevel() 
    {
        int sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    private IEnumerator StartGameCoroutine() 
    {
        _fader.FadeOut(1f);
        _dummyRocket.LevelStartBehaviour();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(GetNextSceneIndex());
        _fader.FadeIn(1f);
    }

}
