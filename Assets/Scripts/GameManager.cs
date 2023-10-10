using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private bool _gamePaused = false;
    private PlayerData _playerData;

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else 
        {
            Object.Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (PlayerManager.instance.GetPlayer(0).HasPressedPause() && !_gamePaused)
        {
            PauseGame();
        }
        else if (PlayerManager.instance.GetPlayer(0).HasPressedPause() && _gamePaused) 
        {
            ResumeGame();
        }
    }

    public void WinCondition() 
    {
        //start game win FX
        //start game win sound
        SceneHandler.instance.NextScene();
    }

    public void LoseCondition() 
    {
        //TOOD: finish lose condition
        //TODO: lose sound FX
        //TOOD: lose visual FX
        //TODO: display lose scene
        SceneHandler.instance.MainMenu();
    }

    public void ResumeGame() 
    {
        AudioManager.instance.PressButton();
        UIManager.instance.DisablePauseMenu();
        _gamePaused = false;
        Time.timeScale = 1;
        Debug.Log("Game Resumed");
    }

    public void PauseGame() 
    {
        UIManager.instance.EnablePauseMenu();
        _gamePaused = true;
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    } 

    public void GoToMainMenu() 
    {
        AudioManager.instance.PressButton();
        SceneHandler.instance.MainMenu();
        _gamePaused = false;
        Time.timeScale = 1;
    }

    public void ResetPlayerData() 
    {
        _playerData.SetDefaultStats();
    }
}
