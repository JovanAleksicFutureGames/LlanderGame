using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    private bool _targetInRange = false;
    private float _rangeDistance = 15f;
    private Transform _target;

    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shotCooldown = 3f;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        _targetInRange = Vector3.Distance(transform.position, _target.position) <= _rangeDistance;
        _muzzlePosition.rotation = transform.rotation;

        if (_targetInRange) 
        {
            
            transform.LookAt(_target.position);
            _shotCooldown -= Time.deltaTime;
            if(_shotCooldown <= 0f)
            {
                Shoot();
            }
        }

    }

    private void Shoot() 
    {
        _shotCooldown = 3f;
        Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.rotation);
    }


}
