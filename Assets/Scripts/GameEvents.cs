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

    #region OnCityCapture_Event
    public event EventHandler<OnCityCaptureEventArgs> OnCityCapture;

    public class OnCityCaptureEventArgs : EventArgs {
        public FactionModule attackingFaction;
        public UnitManager capturedCity;
    }
    #endregion

    public event EventHandler<OnFactionEliminatedEventArgs> OnFactionEliminated;

    public class OnFactionEliminatedEventArgs : EventArgs {
        public FactionModule faction;
        public FactionModule eliminatingFaction;
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

    public void Events_FactionEliminated(FactionModule faction, FactionModule eliminatingFaction)
    {
        Debug.Log(faction.factionName + " Has Been Eliminated by " + eliminatingFaction.factionName);
        OnFactionEliminated?.Invoke(this, new OnFactionEliminatedEventArgs {faction = faction, eliminatingFaction = eliminatingFaction });

        faction.FactionEliminated();
    }

    public void Events_CityCaptured(FactionModule attackingFaction, UnitManager capturedCity)
    {
        Debug.Log(capturedCity.unitName + " has been captured!");
        OnCityCapture?.Invoke(this, new OnCityCaptureEventArgs{ attackingFaction = attackingFaction, capturedCity = capturedCity});
    }

    IEnumerator Events_IncomeTick()
    {
        while(true)
        {
            yield return new WaitForSeconds(incomeTickDelay);
            OnIncomeTick?.Invoke();
        }
    }

    private void update()
    {
        
    }
}
