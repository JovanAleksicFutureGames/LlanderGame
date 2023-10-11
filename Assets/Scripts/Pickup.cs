using UnityEngine;


public class Pickup : MonoBehaviour
{
    public enum PickupType 
    {
        Fuel,
        Health
    }

    [SerializeField] private float _resourceToAdd = 20f;
    [SerializeField] private PickupType _pickupType;
    private MeshRenderer _meshRenderer;
    private MeshCollider _meshCollider;
    private bool _objectActive = true;
    private float _timeSinceDeactivated;
    private float _timeTillReactivation = 5f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        ActivateGameObject();
    }

    private void Update()
    {
        transform.Rotate(transform.up * 15f * Time.deltaTime);
        if (!_objectActive) _timeSinceDeactivated += Time.deltaTime;
        if(_timeSinceDeactivated >= _timeTillReactivation) 
        {
            ActivateGameObject();
        }
    }

    private void DeactivateGameObject() 
    {
        _meshCollider.enabled = false;
        _meshRenderer.enabled = false;
        _objectActive = false;
        _timeSinceDeactivated = 0f;
    }

    public void ActivateGameObject() 
    {
        _meshCollider.enabled = true;
        _meshRenderer.enabled = true;
        _objectActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (_pickupType == PickupType.Fuel)
            {
                other.GetComponent<PlayerController>().PlayerData.AddFuel(_resourceToAdd);
                DeactivateGameObject();

            }
            if (_pickupType == PickupType.Health)
            {
                Debug.Log(other.name);
                other.GetComponent<PlayerController>().PlayerData.AddHealth((int)_resourceToAdd);
                DeactivateGameObject();
            }
        }
    }
}
