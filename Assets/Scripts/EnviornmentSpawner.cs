using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnviornmentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _envPrefabs;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;


    private GameObject _spawnedObj;
    private int _prefabToSpawn;

    private void Awake()
    {
        _prefabToSpawn = Random.Range(0, _envPrefabs.Length);
        _spawnedObj = Instantiate(_envPrefabs[_prefabToSpawn], transform.position, transform.rotation);
        _spawnedObj.layer = 3;
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject() 
    {
        _meshFilter = _spawnedObj.GetComponent<MeshFilter>();
        _meshRenderer = _spawnedObj.GetComponent<MeshRenderer>();
    }
}
