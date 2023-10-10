using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Rigidbody _rigidBody;
    [SerializeField] private float _moveSpeed = 15f;
    [SerializeField] private GameObject _explosionPrefab;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidBody.AddForce(transform.forward * _moveSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("Enemy"))
        {
            GameObject instance = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(instance, .5f);
            Destroy(this.gameObject, .75f);
        }
    }
}
