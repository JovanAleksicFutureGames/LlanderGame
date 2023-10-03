using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataContainer : MonoBehaviour, ISaveable
{
    public string m_UniqueID;
    [SerializeField] private string objectName;
    [SerializeField] private int level;
    [SerializeField] private string characterClass;

    public string UniqueID { get { return m_UniqueID; } set { m_UniqueID = value; } }

    public void OnLoad(Dictionary<string, object> data)
    {
        objectName = (string)data[nameof(objectName)];
        level = (int)data[nameof(level)];
        characterClass = (string)data[nameof(characterClass)];
    }

    public Dictionary<string, object> OnSave()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();
        data.Add(nameof(objectName), objectName);
        data.Add(nameof(level), level);
        data.Add(nameof(characterClass), characterClass);
        return data;
    }
}
