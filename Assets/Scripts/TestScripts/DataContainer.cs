using UnityEngine;

public class DataContainer : MonoBehaviour, ISaveable
{
    [SerializeField] private string objectName;
    [SerializeField] private int level;
    [SerializeField] private int characterClass;

}
