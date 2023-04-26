using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactionModule : MonoBehaviour
{
    public bool eliminated = false;
    public int gold = 0;
    public int kothScore = 0;
    public string factionName;
    public UnitManager capital;
    public List<UnitManager> ownedCities;
    public Color factionColor;

    private void Start()
    {
        if (capital == null)
        {
            if (ownedCities.Count != 0)
            {
                capital = ownedCities[0];
                ownedCities[0].isCapital = true;
            }
            else
            {
                Debug.Log("No city exists for faction " + factionName);
            }
        }

        

        GameEvents.instance.OnCityCapture += CityCaptured;
        GameEvents.instance.OnIncomeTick += TickIncome;
    }

    public void FactionEliminated()
    {
        eliminated = true;
        for (int i = 0; i < ownedCities.Count; i++)
        {
            GameEvents.instance.Events_CityCaptured(StaticPropertyVariables.ins.MaraudersFaction, ownedCities[i]);
        }
    }

    public void CityCaptured(object sender, GameEvents.OnCityCaptureEventArgs eArgs)
    {
        UnitManager capturedCity = eArgs.capturedCity;

        if (eArgs.attackingFaction == this)
        {
            AddCity(capturedCity);
        }
        else if (capturedCity.ownerFaction == this)
        {
            capturedCity.CityCaptured(eArgs.attackingFaction);
            ownedCities.Remove(capturedCity);

            if (capturedCity == capital)
            {
                GameEvents.instance.Events_FactionEliminated(capturedCity.ownerFaction, eArgs.attackingFaction);
            }
        }
    }

    public void AddCity(UnitManager city)
    {
        ownedCities.Add(city);
        AssignFactionColorToCity(city);
    }

    private void AssignFactionColorToCity(UnitManager city)
    {
        if (city.ownerFaction == this)
        {
            city.unitSprite.material.SetColor("_FactionColor", factionColor);
        }
        else
        {
            Debug.Log("AssignFactionColorToCity(): City faction doesn't match the faction module");
        }
    }

    private void TickIncome()
    {
        for (int i = 0; i < ownedCities.Count; i++)
        {
            gold += ownedCities[i].goldIncome;
        }
    }
}
