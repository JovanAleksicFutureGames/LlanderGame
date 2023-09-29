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
        _howToPlaySCreen.SetActive(true);
    }

    public void HideHowToPlay() 
    {
        _howToPlaySCreen.SetActive(false);
    }
}
