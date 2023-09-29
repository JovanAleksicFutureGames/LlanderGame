using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour, ISaveable
{
    [SerializeField] private string name;
    [SerializeField] private int level;
    [SerializeField] private int characterClass;

    
}
