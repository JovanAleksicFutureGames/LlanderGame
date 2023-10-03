using System.Collections;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public bool _isAlive;

    private PlayerInput _input;
    private PlayerMotor _playerMotor;

    [field: SerializeField] public float _rotationSpeed { get; private set; }
    [field: SerializeField] public float _forceAmount { get; private set; }
    [field: SerializeField] public float _fuelAmount { get; private set; }
    [field: SerializeField] public float _fuelDrainRate { get; private set; }
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldownTimer;
    [SerializeField] private float _jumpFuelCost = 15f;

    [Header("Components")]
    [SerializeField] private ParticleSystem _exhaust;
    [SerializeField] private GameObject _mainBody;

    private float xInput;

    //temp - will migrate to Score and Game Manager scripts

    [SerializeField] GameObject _explosionVFX;

    private void Awake()
    {
        _input = new PlayerInput();
        _playerMotor = GetComponent<PlayerMotor>();
        _playerMotor.EnableGravity();
        _isAlive = true;
        _mainBody.SetActive(true);
        _exhaust.gameObject.SetActive(false);
    }

    private void Update()
    {
        _jumpCooldownTimer -= Time.deltaTime;
        RotateShip();
    }

    private void FixedUpdate()
    {
        Boost();
        Jump();
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
            DrainFuelOverTime();
            _exhaust.gameObject.SetActive(true);
            UIManager.instance.UpdateFuelDisplay();
        }
        else
            _exhaust.gameObject.SetActive(false);
    }

    private void DrainFuelOverTime()
    {
        _fuelAmount -= _fuelDrainRate * Time.deltaTime;
        if (_fuelAmount <= 0) _fuelAmount = 0;
    }

    private void DrainFuel(float amountToDrain)
    {
        _fuelAmount -= amountToDrain;
        UIManager.instance.UpdateFuelDisplay();
    }

    private void Jump()
    {
        if (_input.Player.Jump.IsPressed() && _jumpCooldownTimer <= 0f && _fuelAmount >= _jumpFuelCost)
        {
            _jumpCooldownTimer = 3f;
            _playerMotor.JumpForce(_jumpForce);
            DrainFuel(_jumpFuelCost);
        }
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    /*    private void ResetPlayer()
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            _mainBody.SetActive(true);
            _playerMotor.EnableGravity();
            _fuelAmount = 100f;
            UIManager.instance.UpdateFuelDisplay();
        }*/


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //socring and end game logic goes here
            StartCoroutine(PlayerDeath());
        }
        else if (collision.gameObject.layer == 6)
        {
            GameManager.instance.WinCondition();
        }
    }

    //public methods

    public void AddFuel(float fuelToAdd)
    {
        _fuelAmount += fuelToAdd;
        if (_fuelAmount >= 100)
        {
            _fuelAmount = 100;
        }
    }

    public bool HasPressedPause() 
    {
        return _input.Player.Pause.WasPerformedThisFrame();
    }
    //coroutines

    private IEnumerator PlayerDeath()
    {

        GameManager.instance.DecrementLives();
        _playerMotor.DisableGravity();
        GameObject explosionInstance = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        _isAlive = false;
        _mainBody.SetActive(false);
        _playerMotor.StopMovement();
        yield return new WaitForSeconds(0.55f);
        Destroy(explosionInstance);
        if (GameManager.instance.Lives <= 0)
        {
            GameManager.instance.LoseCondition();
        }
        yield return new WaitForSeconds(0.1f);
        SceneHandler.instance.RestartCurrentLevel();

    }

    public bool SaveData<T>(string relativePath, T data)
    {
        throw new System.NotImplementedException();
    }

    public T LoadData<T>(string relativePath)
    {
        throw new System.NotImplementedException();
    }
}
