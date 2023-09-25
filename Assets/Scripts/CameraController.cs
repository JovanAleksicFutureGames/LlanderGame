using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Transform _trackedObject;
    [SerializeField] private Vector3 _offset;


    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = _trackedObject.position + _offset;       
    }
}
