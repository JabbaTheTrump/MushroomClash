using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    public event EventHandler<OnCityCaptureEventArgs> OnCityCapture;
    public class OnCityCaptureEventArgs : EventArgs {
        public UnitManager attacker;
        public UnitManager capturedCity;
    }

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    public void Events_CityCaptured(UnitManager attacker, UnitManager capturedCity)
    {
        Debug.Log(capturedCity.unitName + " has been captured!");
        OnCityCapture?.Invoke(this, new OnCityCaptureEventArgs{attacker = attacker, capturedCity = capturedCity});
    }

    private void update()
    {
        
    }
}
