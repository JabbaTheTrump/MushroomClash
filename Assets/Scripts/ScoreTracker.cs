using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int team1CityCount = 1;
    public int team2CityCount = 1;

    public FactionModule team1FactionModule;
    public FactionModule team2FactionModule;

    void Start()
    {
        GameEvents.instance.OnCityCapture += UpdateCityCount;
    }

    void Update()
    {
        
    }

    void UpdateCityCount(object sender, GameEvents.OnCityCaptureEventArgs eArgs)
    {
        team1CityCount = team1FactionModule.ownedCities.Count;
        team2CityCount = team2FactionModule.ownedCities.Count;
    }
}
