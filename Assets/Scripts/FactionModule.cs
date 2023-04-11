using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionModule : MonoBehaviour
{
    public int gold;
    public int kothScore = 0;
    public string factionName;
    public UnitManager capital;
    public List<UnitManager> ownedCities;
    public Color factionColor;

    private void Start()
    {
        GameEvents.instance.OnCityCapture += CityCaptured;
    }

    public void CityCaptured(object sender, GameEvents.OnCityCaptureEventArgs eArgs)
    {
        UnitManager capturedCity = eArgs.capturedCity;
        if (eArgs.attacker.ownerFaction == this)
        {
            AddCity(capturedCity);
        }
        else if (capturedCity.ownerFaction == this)
        {
            capturedCity.CityCaptured(eArgs.attacker.ownerFaction);
            ownedCities.Remove(capturedCity);
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
}
