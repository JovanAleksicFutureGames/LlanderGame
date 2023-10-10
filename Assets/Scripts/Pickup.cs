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

    private void Update()
    {
        transform.Rotate(transform.up * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (_pickupType == PickupType.Fuel)
            {
                other.GetComponent<PlayerController>().PlayerData.AddFuel(_resourceToAdd);
                Destroy(gameObject, 0.1f);
            }
            if (_pickupType == PickupType.Health)
            {
                other.GetComponent<PlayerController>().PlayerData.AddHealth((int)_resourceToAdd);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
