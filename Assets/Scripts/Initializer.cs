using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public static Initializer instance;
    private void Awake()
    {
        instance = this;
    }
}
