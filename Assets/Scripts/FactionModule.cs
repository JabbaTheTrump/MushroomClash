using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FactionModule : MonoBehaviour
{
    public bool eliminated = false;
    public int gold = 0;
    public int goldIncome = 100;
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

        
        GameEvents.instance.OnIncomeTick += TickIncome;
    }

    private void Update()
    {
        CalculateIncome();
    }

    private void CalculateIncome()
    {
        int income = 0;

        for (int i = 0; i < ownedCities.Count; i++)
        {
            income += ownedCities[i].goldIncome;
        }

        goldIncome = income;
    }

    public void FactionEliminated(FactionModule eliminatingFaction)
    {
        Debug.Log(factionName + " Has Been Eliminated by " + eliminatingFaction.factionName);
        GameEvents.instance.Events_FactionEliminated(this, eliminatingFaction);
        eliminated = true;

        for (int i = 0; i < ownedCities.Count; i++)
        {
            GameEvents.instance.Events_CityCaptured(StaticPropertyVariables.ins.MaraudersFaction, ownedCities[i]);
        }
    }

    public void AddCity(UnitManager city)
    {
        ownedCities.Add(city);
        AssignFactionColorToCity(city);
    }

    public void RemoveCity(UnitManager city)
    {
        ownedCities.Remove(city);
    }

    private void AssignFactionColorToCity(UnitManager city)
    {
        city.unitSprite.material.SetColor("_FactionColor", factionColor);
    }

    public void TickIncome()
    {
        if (gold < 1000000)
        {
            gold += goldIncome;
        }
    }
}
