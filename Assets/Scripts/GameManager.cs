using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [field: SerializeField] public int Lives { get; private set; }
    private bool _gamePaused = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (PlayerController.instance.HasPressedPause() && !_gamePaused)
        {
            PauseGame();
        }
        else if (PlayerController.instance.HasPressedPause() && _gamePaused) 
        {
            ResumeGame();
        }
    }

    public void WinCondition() 
    {
        Debug.Log("You have wom the game.");
    }

    public void LoseCondition() 
    {
        Debug.Log("You have lost the game");
    }

    public void DecrementLives() 
    {
        Lives--;
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

    public void ReturnToMainMenu() 
    {
        Debug.Log("Returning to the main menu");
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    
}
