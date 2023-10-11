using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _fuelAmountText;
    [SerializeField] private Image _fuelGaugeFill;
    [SerializeField] private TextMeshProUGUI _heathAmountText;
    [SerializeField] private Image _healthGaugeFill;

    private void Awake()
    {
        instance = this;
        DisablePauseMenu();
        DisableVictoryScreen();
    }

    private void Start()
    {
        UpdateFuelDisplay();
        UpdateHealthDisplay();
    }

    public void UpdateFuelDisplay()
    {
        _fuelAmountText.text = PlayerManager.instance.GetPlayer(0).PlayerData.FuelAmount.ToString("0");
        _fuelGaugeFill.fillAmount = PlayerManager.instance.GetPlayer(0).PlayerData.FuelAmount / 100f;
    }

    public void UpdateHealthDisplay() 
    {
        _heathAmountText.text = PlayerManager.instance.GetPlayer(0).PlayerData.Health.ToString("0");
        _healthGaugeFill.fillAmount = PlayerManager.instance.GetPlayer(0).PlayerData.Health / 5f;
    }

    public void EnablePauseMenu() 
    {
        _pauseMenu.SetActive(true);
    }

    public void EnableVictoryScreen() 
    {
        _victoryScreen.SetActive(true);
    }

    public void DisableVictoryScreen() 
    {
        _victoryScreen.SetActive(false);
    }

    public void DisablePauseMenu() 
    {
        _pauseMenu.SetActive(false);
    }
}
