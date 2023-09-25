using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public bool isAlive;

    private PlayerInput _input;
    private PlayerMotor _playerMotor;

    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _forceAmount = 5f;
    [SerializeField] private float _fuelAmount = 100f;
    [SerializeField] private float _fuelDrainRate = .5f;

    [Header("Components")]
    [SerializeField] private ParticleSystem _exhaust;
    [SerializeField] private GameObject _mainBody;

    private float xInput;

    //temp - will migrate to Score and Game Manager scripts

    [SerializeField] GameObject _explosionVFX;
    [SerializeField] TextMeshProUGUI _fuelAmountText;
    [SerializeField] Image _fuelGaugeFill;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        isAlive = true;
        _mainBody.SetActive(true);
        _input = new PlayerInput();
        _playerMotor = GetComponent<PlayerMotor>();
        _playerMotor.EnableGravity();
        _exhaust.gameObject.SetActive(false);
    }

    private void Update()
    {
        RotateShip();
        UpdateUI();

    }

    private void FixedUpdate()
    {
        Boost();
    }


    private void RotateShip()
    {
        xInput = -_input.Player.Rotate.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.forward * xInput * _rotationSpeed * Time.fixedDeltaTime);
    }

    private void Boost() 
    {
        if (_input.Player.Boost.IsPressed() && _fuelAmount > 0)
        {
            _playerMotor.AddUpForce(_forceAmount);
            DrainFuel();
            _exhaust.gameObject.SetActive(true);
        }
        else
            _exhaust.gameObject.SetActive(false);
    }

    private void DrainFuel() 
    {
       _fuelAmount -= _fuelDrainRate * Time.deltaTime;
       if(_fuelAmount <= 0) _fuelAmount = 0;
    }

    private void UpdateUI() 
    {
        _fuelAmountText.text = _fuelAmount.ToString("0");
        _fuelGaugeFill.fillAmount = _fuelAmount / 100f;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    //public methods

    public void AddFuel(float fuelToAdd) 
    {
        _fuelAmount += fuelToAdd;
        if(_fuelAmount >= 100) 
        {
            _fuelAmount = 100;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3) 
        {
            //socring and end game logic goes here
            StartCoroutine(PlayerDeath());
        }
    }

    //coroutines

    private IEnumerator PlayerDeath() 
    {
        GameManager.instance.DecrementLives();
        _playerMotor.DisableGravity();
        GameObject explosionInstance = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        isAlive = false;
        _mainBody.SetActive(false);
        yield return new WaitForSeconds(0.55f);
        Destroy(explosionInstance);
        SceneManager.LoadScene("SampleScene");
    }
}
