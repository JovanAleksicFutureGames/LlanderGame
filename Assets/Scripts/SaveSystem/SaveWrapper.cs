using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWrapper : MonoBehaviour
{
    private PlayerInput _input;


    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        if (_input.Player.Save.WasPerformedThisFrame()) 
        {
            
        }
        if (_input.Player.Load.WasPerformedThisFrame()) 
        {
            
        }
    }
}
