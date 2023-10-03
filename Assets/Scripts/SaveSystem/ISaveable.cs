using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    Dictionary<string, object> OnSave();
    void OnLoad(Dictionary<string, object> data);
    string UniqueID { get; set; }
}
