using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    //TODO: Victory conditions
    //TODO: game states
    //TODO: end game
    //TODO: quitting the applicaton
    //TODO: Main Menu
}
