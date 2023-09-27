using UnityEngine;

public class DummyRocket : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void LevelStartBehaviour() 
    {
        _rigidBody.AddForce(transform.up * 15f, ForceMode.Impulse);
    }
}
