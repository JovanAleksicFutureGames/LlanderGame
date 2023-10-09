using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnviornmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _envPrefabs;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;


    private GameObject _spawnedObj;
    private int _prefabToSpawn;

    private void Awake()
    {
        _prefabToSpawn = Random.Range(0, _envPrefabs.Length);
        _spawnedObj = Instantiate(_envPrefabs[_prefabToSpawn], transform.position, transform.rotation);
        _spawnedObj.layer = 3;
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshCollider = GetComponent<MeshCollider>();
        _meshCollider.convex = true;
    }

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject() 
    {
        _meshFilter = _spawnedObj.GetComponent<MeshFilter>();
        _meshRenderer = _spawnedObj.GetComponent<MeshRenderer>();
/*        _meshCollider.enabled = false;
        RaycastHit hit;
        bool isIntersecting = Physics.Raycast(_spawnedObj.transform.position, Vector3.up, out hit, 5f);
        if (isIntersecting) 
        {
            Destroy(_spawnedObj);
            _meshFilter = _spawnedObj.GetComponent<MeshFilter>();
            _meshRenderer = _spawnedObj.GetComponent<MeshRenderer>();
            _meshCollider.enabled = true;
        }*/
    }
}
