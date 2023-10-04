using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log(other.name);
            if (_pickupType == PickupType.Fuel)
            {
                PlayerData.instance.AddFuel(_resourceToAdd);
                Destroy(gameObject, 0.1f);
            }
            if (_pickupType == PickupType.Health)
            {
                PlayerData.instance.AddHealth((int)_resourceToAdd);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
