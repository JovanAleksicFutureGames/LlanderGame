using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    private bool _targetInRange = false;
    private float _rangeDistance = 15f;
    private PlayerData _target;

    [SerializeField] private Transform _muzzlePosition;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _shotCooldown = 3f;

    private void Awake()
    {
        //TOOD: FIX EVERYTHING YOU BROKE
    }

    private void Update()
    {
        _targetInRange = Vector3.Distance(transform.position, _target.GetComponent<Transform>().position) <= _rangeDistance;

        if (_targetInRange) 
        {
            
            transform.LookAt(_target.GetComponent<Transform>().position);
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