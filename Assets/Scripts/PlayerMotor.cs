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

    public void JumpForce(float forceToAdd) 
    {
        _rigidBody.AddForce(transform.up * forceToAdd, ForceMode.Impulse);
    }

    public void DisableGravity() 
    {
        _rigidBody.useGravity = false;
    }

    public void EnableGravity() 
    {
        _rigidBody.useGravity = true;
    }

    public void StopMovement() 
    {
        _rigidBody.velocity = Vector3.zero;
    }

}
