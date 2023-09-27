using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    private DummyRocket dummyRocket;
    private void Awake()
    {
        instance = this;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            dummyRocket = GameObject.FindObjectOfType<DummyRocket>();
        }
        else 
            dummyRocket = null;
    }

    public void NextScene()
    {
        Debug.Log("Current scene loaded: " + SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            MainMenu();
        }
        else
            SceneManager.LoadSceneAsync(GetNexSceneIndex());
    }

    public int GetNexSceneIndex() 
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentIndex);
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

    private IEnumerator StartGameCoroutine() 
    {
        dummyRocket.LevelStartBehaviour();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(GetNexSceneIndex());
    }

}
