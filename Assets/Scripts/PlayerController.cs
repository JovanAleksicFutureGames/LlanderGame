using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private PlayerInput input;
    private PlayerMotor playerMotor;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float forceAmount = 5f;
    [SerializeField] private float fuelAmount = 100f;
    [SerializeField] private float fuelDrainRate = .5f;

    private float xInput;

    //temp - will migrate to Score and Game Manager scripts

    [SerializeField] TextMeshProUGUI fuelAmountText;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        input = new PlayerInput();
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        RotateShip();
        Boost();
        UpdateUI();

    }


    private void RotateShip()
    {
        xInput = -input.Player.Rotate.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.forward * xInput * rotationSpeed * Time.deltaTime);
    }

    private void Boost() 
    {
        if (input.Player.Boost.IsPressed() && fuelAmount > 0) 
        {
            playerMotor.AddUpForce(forceAmount);
            DrainFuel();
        }
    }

    private void DrainFuel() 
    {
       fuelAmount -= fuelDrainRate * Time.deltaTime;
       if(fuelAmount <= 0) fuelAmount = 0;
    }

    private void UpdateUI() 
    {
        fuelAmountText.text = fuelAmount.ToString("0");
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    //public methods

    public void AddFuel(float fuelToAdd) 
    {
        fuelAmount += fuelToAdd;
        if(fuelAmount >= 100) 
        {
            fuelAmount = 100;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3) 
        {
            //socring and end game logic goes here
            SceneManager.LoadScene("SampleScene");
        }
    }


}
