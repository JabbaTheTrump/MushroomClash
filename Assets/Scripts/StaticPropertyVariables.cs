using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticPropertyVariables : MonoBehaviour
{
    public static StaticPropertyVariables ins;
    [Range(0.0f, 10.0f)] public float secondsPerUpdate = 1f;
    [Range(0.0f, 10.0f)] public float battleSpeed = 1f;
    public FactionModule MaraudersFaction;

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(this);
        }
        else
        {
            ins = this;
        }
    }
}
