using System.Collections;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public bool _isAlive;
    private PlayerInput _input;
    private PlayerMotor _playerMotor;
    [field: SerializeField] public PlayerData PlayerData { get; private set; }
    [field: SerializeField] public float _rotationSpeed { get; private set; }
    [field: SerializeField] public float _forceAmount { get; private set; }

    [field: SerializeField] public float _fuelDrainRate { get; private set; }
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldownTimer;
    [SerializeField] private float _jumpFuelCost = 15f;

    [Header("Components")]
    [SerializeField] private ParticleSystem _exhaust;
    [SerializeField] private ParticleSystem _jumpFX;
    [SerializeField] private ParticleSystem _damageFX;
    [SerializeField] private GameObject _mainBody;

    private float _xInput;
    private float _collisionCooldown = 1f;
    [SerializeField] GameObject _explosionVFX;

    private void Awake()
    {
        _input = new PlayerInput();
        _playerMotor = GetComponent<PlayerMotor>();
        _playerMotor.EnableGravity();
        _isAlive = true;
        _mainBody.SetActive(true);
        _exhaust.gameObject.SetActive(false);
        _jumpFX.gameObject.SetActive(false);
        _damageFX.gameObject.SetActive(false);
        if(PlayerData.Health <= 0 && PlayerData.FuelAmount <= 0) 
        {
            PlayerData.SetDefaultStats();
        }
    }

    private void Update()
    {
        _jumpCooldownTimer -= Time.deltaTime;
        _collisionCooldown -= Time.deltaTime;
        RotateShip();
        if(PlayerData.FuelAmount <= 0) 
        {
            LoseControl();
        }
    }

    private void FixedUpdate()
    {
        Boost();
        Jump();
    }


    private void RotateShip()
    {
        if(PlayerData.FuelAmount > 0) 
        {
            _xInput = -_input.Player.Rotate.ReadValue<Vector2>().x;
            transform.Rotate(Vector3.forward * _xInput * _rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void LoseControl() 
    {
        transform.Rotate(Vector3.forward * 10f * _rotationSpeed * Time.deltaTime);
    }

    private void Boost()
    {
        if (_input.Player.Boost.IsPressed() && PlayerData.FuelAmount > 0)
        {
            _playerMotor.AddUpForce(_forceAmount);
            DrainFuelOverTime();
            _exhaust.gameObject.SetActive(true);
        }
        else 
        {
            _exhaust.gameObject.SetActive(false);
        }
    }

    private void DrainFuelOverTime()
    {
        PlayerData.DrainFuel(_fuelDrainRate, Time.deltaTime);
        if (PlayerData.FuelAmount <= 0) PlayerData.SetFuel(0);
    }

    private void DrainFuel(float amountToDrain)
    {
        PlayerData.SubtractFuel(amountToDrain);
    }

    private void Jump()
    {
        if (_input.Player.Jump.IsPressed() && _jumpCooldownTimer <= 0f && PlayerData.FuelAmount >= _jumpFuelCost)
        {
            _jumpCooldownTimer = 1f;
            AudioManager.instance.PlayerPlayJumpSound();
            StartCoroutine(PlayJumpFX());
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
            _collisionCooldown = 3;
            StartCoroutine(PlayDamageFX());
            PlayerData.DecrementHealth();
            AudioManager.instance.PlayerTakeDamage();
            if (PlayerData.Health <= 0) 
            {
                PlayerData.SetHealth(0);
                StartCoroutine(PlayerDeath(PlayerData));
            }

        }
        if(PlayerData.FuelAmount < .5f)
        {
            StartCoroutine(PlayerDeath(PlayerData));
        }
    }

    //public methods

    public void AddFuel(float fuelToAdd)
    {
        PlayerData.AddFuel(fuelToAdd);
        if (PlayerData.FuelAmount >= 100)
        {
            PlayerData.SetFuel(100);
        }
    }

    public bool HasPressedPause() 
    {
        return _input.Player.Pause.WasPerformedThisFrame();
    }

    //coroutines

    private IEnumerator PlayerDeath(PlayerData data)
    {
        data.SetHealth(0);
        _playerMotor.DisableGravity();
        GameObject explosionInstance = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        _isAlive = false;
        _mainBody.SetActive(false);
        _playerMotor.StopMovement();
        yield return new WaitForSeconds(0.55f);
        Destroy(explosionInstance);
        yield return new WaitForSeconds(0.1f);
        ResetPlayer();

    }

    private IEnumerator PlayJumpFX()
    {
        _jumpFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(.2f);
        _jumpFX.gameObject.SetActive(false);
    }

    private IEnumerator PlayDamageFX() 
    {
        _damageFX.gameObject.SetActive(true);
        yield return new WaitForSeconds(.75f);
        _damageFX.gameObject.SetActive(false);
    }
}
