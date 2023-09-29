using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    bool SaveData<T>(string RelativePath, T Data, bool Encrypted); //do I need to encrypt this?



}
