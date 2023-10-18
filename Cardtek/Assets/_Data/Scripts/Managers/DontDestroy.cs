using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        //This makes that the object dosent destroy in loads
        DontDestroyOnLoad(gameObject);
    }
}
