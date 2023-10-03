using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private bool _gamePaused = false;

    private void Awake()
    {
        instance = this;
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
        Debug.Log("You have lost the game");
    }

    public void DecrementLives() 
    {
        PlayerManager.instance.GetPlayer(0).PlayerData.DecrementLives();
        UIManager.instance.DisplayLives();
    }

    
    public void ResumeGame() 
    {
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
}
