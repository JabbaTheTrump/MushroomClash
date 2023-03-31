using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPropertyVariables : MonoBehaviour
{
    public static StaticPropertyVariables instance = null;
    public float secondsPerUpdate = 1f;

    void Start()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
