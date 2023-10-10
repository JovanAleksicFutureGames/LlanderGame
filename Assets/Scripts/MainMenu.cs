using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _howToPlaySCreen;

    private void Awake()
    {
        HideHowToPlay();
    }

    public void ShowHowToPlay() 
    {
        AudioManager.instance.PressButton();
        _howToPlaySCreen.SetActive(true);
    }

    public void HideHowToPlay() 
    {
        AudioManager.instance.PressButton();
        _howToPlaySCreen.SetActive(false);
    }

    public void QuitGame() 
    {
        AudioManager.instance.PressButton();
        Application.Quit();
    }
}
