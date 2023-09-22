using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void AddUpForce(float forceToAdd) 
    {
        rigidBody.AddForce(transform.up * forceToAdd, ForceMode.Force);
    }
}
