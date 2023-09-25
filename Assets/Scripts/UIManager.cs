using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _LivesText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _fuelAmountText;
    [SerializeField] private Image _fuelGaugeFill;

    private void Awake()
    {
        instance = this;
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
}
