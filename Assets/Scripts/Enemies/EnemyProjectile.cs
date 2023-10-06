using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed = 15f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidBody.AddForce(transform.forward * _moveSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }

}
