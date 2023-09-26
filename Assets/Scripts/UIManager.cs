using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _LivesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _fuelAmountText;
    [SerializeField] private Image _fuelGaugeFill;

    private void Awake()
    {
        instance = this;
        DisablePauseMenu();
    }

    private void Start()
    {
        UpdateFuelDisplay();
        DisplayLives();
    }

    public void UpdateFuelDisplay()
    {
        _fuelAmountText.text = PlayerController.instance._fuelAmount.ToString("0");
        _fuelGaugeFill.fillAmount = PlayerController.instance._fuelAmount / 100f;
    }

    public void DisplayLives() 
    {
        _LivesText.text = "Lives: " + GameManager.instance.Lives.ToString();
    }

    public void EnablePauseMenu() 
    {
        _pauseMenu.SetActive(true);
    }

    public void DisablePauseMenu() 
    {
        _pauseMenu.SetActive(false);
    }
}
