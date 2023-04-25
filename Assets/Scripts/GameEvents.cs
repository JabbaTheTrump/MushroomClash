using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void IncomeTickDel();
    public static GameEvents instance;
    public float incomeTickDelay = 1;
    public Action OnIncomeTick;
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

        StartCoroutine(Events_IncomeTick());
    }

    public void Events_CityCaptured(UnitManager attacker, UnitManager capturedCity)
    {
        Debug.Log(capturedCity.unitName + " has been captured!");
        OnCityCapture?.Invoke(this, new OnCityCaptureEventArgs{attacker = attacker, capturedCity = capturedCity});
    }

    IEnumerator Events_IncomeTick()
    {
        while(true)
        {
            new WaitForSeconds(incomeTickDelay);
            Debug.Log("Income tick");
            OnIncomeTick?.Invoke();
        }
    }

    private void update()
    {
        
    }
}
