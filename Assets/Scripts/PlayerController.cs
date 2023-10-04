using System.Collections;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public bool _isAlive;
    private PlayerInput _input;
    private PlayerMotor _playerMotor;
    public PlayerData PlayerData { get; private set; }
    [field: SerializeField] public float _rotationSpeed { get; private set; }
    [field: SerializeField] public float _forceAmount { get; private set; }

    [field: SerializeField] public float _fuelDrainRate { get; private set; }
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldownTimer;
    [SerializeField] private float _jumpFuelCost = 15f;

    [Header("Components")]
    [SerializeField] private ParticleSystem _exhaust;
    [SerializeField] private GameObject _mainBody;

    private float _xInput;
    private float _collisionCooldown = 1f;

    //temp - will migrate to Score and Game Manager scripts

    [SerializeField] GameObject _explosionVFX;

    private void Awake()
    {
        _input = new PlayerInput();
        _playerMotor = GetComponent<PlayerMotor>();
        PlayerData = GetComponent<PlayerData>();
        _playerMotor.EnableGravity();
        _isAlive = true;
        _mainBody.SetActive(true);
        _exhaust.gameObject.SetActive(false);
    }

    private void Start()
    {
        //SaveWrapper.instance.LoadGame();

    }

    private void Update()
    {
        _jumpCooldownTimer -= Time.deltaTime;
        _collisionCooldown -= Time.deltaTime;
        RotateShip();
    }

    private void FixedUpdate()
    {
        Boost();
        Jump();
    }


    private void RotateShip()
    {
        _xInput = -_input.Player.Rotate.ReadValue<Vector2>().x;
        transform.Rotate(Vector3.forward * _xInput * _rotationSpeed * Time.fixedDeltaTime);
    }

    private void Boost()
    {
        if (_input.Player.Boost.IsPressed() && PlayerData._fuelAmount > 0)
        {
            _playerMotor.AddUpForce(_forceAmount);
            DrainFuelOverTime();
            _exhaust.gameObject.SetActive(true);
        }
        else
            _exhaust.gameObject.SetActive(false);
    }

    private void DrainFuelOverTime()
    {
        PlayerData.DrainFuel(_fuelDrainRate, Time.deltaTime);
        if (PlayerData._fuelAmount <= 0) PlayerData.SetFuel(0);
    }

    private void DrainFuel(float amountToDrain)
    {
        PlayerData.SubtractFuel(amountToDrain);
    }

    private void Jump()
    {
        if (_input.Player.Jump.IsPressed() && _jumpCooldownTimer <= 0f && PlayerData._fuelAmount >= _jumpFuelCost)
        {
            _jumpCooldownTimer = 1f;
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

    private void ResetPlayer()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        _mainBody.SetActive(true);
        _playerMotor.EnableGravity();
        PlayerData.SetFuel(100f);
        PlayerData.SetHealth(5);
        transform.position = new Vector3(-12.25f, -2.25f, 0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 && _collisionCooldown <= 0)
        {
            //socring and end game logic goes here
            //Done: Cooldown that prevents us from colliding with multiple colliders
            //TODO: Blinking effects
            //TODO: Instantiate(collision effect)
            _collisionCooldown = 3;
            PlayerData.instance.DecrementHealth();
            if(PlayerData.instance.Health <= 0) 
            {
                PlayerData.instance.SetHealth(0);
                StartCoroutine(PlayerDeath());
            }

        }
        if(collision.gameObject.layer == 3 && PlayerData.instance._fuelAmount < .5f)
        {
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
        PlayerData.AddFuel(fuelToAdd);
        if (PlayerData._fuelAmount >= 100)
        {
            PlayerData.SetFuel(100);
        }
    }

    public bool HasPressedPause() 
    {
        return _input.Player.Pause.WasPerformedThisFrame();
    }
    //coroutines

    private IEnumerator PlayerDeath()
    {
        PlayerData.instance.SetHealth(0);
        _playerMotor.DisableGravity();
        GameObject explosionInstance = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        _isAlive = false;
        _mainBody.SetActive(false);
        GameManager.instance.DecrementLives();
        _playerMotor.StopMovement();
        yield return new WaitForSeconds(0.55f);
        Destroy(explosionInstance);
        if (PlayerManager.instance.GetPlayer(0).PlayerData.Lives <= 0)
        {
            PlayerManager.instance.SetLives(0);
            GameManager.instance.LoseCondition();
        }
        //SaveWrapper.instance.SaveGame();
        yield return new WaitForSeconds(0.1f);
        ResetPlayer();

    }
}
