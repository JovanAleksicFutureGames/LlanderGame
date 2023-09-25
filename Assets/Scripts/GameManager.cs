using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Lives { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    

    public void VictoryCondition() 
    {

    }

    public void LoseCondition() 
    {

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
