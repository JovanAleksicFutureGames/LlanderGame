using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [field: SerializeField] public int Lives { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
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

    public void ReturnToMainMenu() 
    {

    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    
}
