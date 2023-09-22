using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            PlayerController.instance.AddFuel(fuelAmount);
            Destroy(gameObject, 0.1f);
        }
    }
}
