using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void AddUpForce(float forceToAdd) 
    {
        _rigidBody.AddForce(transform.up * forceToAdd, ForceMode.Force);
    }

    public void DisableGravity() 
    {
        _rigidBody.useGravity = false;
    }

    public void EnableGravity() 
    {
        _rigidBody.useGravity = true;
    }

}
