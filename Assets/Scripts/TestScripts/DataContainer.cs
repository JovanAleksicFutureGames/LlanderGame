using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataContainer : MonoBehaviour, ISaveable
{
    public object CaptureState()
    {
        throw new System.NotImplementedException();
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}
