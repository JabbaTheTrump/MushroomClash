using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SM_MilitaryStateManager : MonoBehaviour
{
    public FactionModule factionModule;
    Dictionary<UnitManager,float> attackTargetsDictionary;
    UnitManager sendingCity;
    UnitManager targetCity;
    
    [Range(0.1f, 10)] public float distanceScoreMultiplier = 1;
    [Range(0.1f, 10)] public float cityManpowerScoreMultiplier = 1; 

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SendArmy()
    {
        ArmyCreation.instance.CreateArmy(sendingCity.manPower, sendingCity, targetCity);
    }

    public void Attack_DetermineOriginCity()
    {
        sendingCity = AI_Evaluators.ins.FindCityWithMostManpower(factionModule);
    }

    public UnitManager Attack_DetermineTargetCity()
    {
        Dictionary<UnitManager, float> manpowerScore = AI_Evaluators.ins.GetEnemyCitiesManpower(sendingCity);
        Dictionary<UnitManager, float> distanceScore = AI_Evaluators.ins.GetEnemyCityDistances(sendingCity, manpowerScore);

        //Set the attack dictionary like so - City : Attractiveness Score
        attackTargetsDictionary = manpowerScore;
        attackTargetsDictionary = manpowerScore.ToDictionary(kvp => kvp.Key, kvp => 0f);


        //Manpower && capital score reduction
        foreach (KeyValuePair<UnitManager, float> pair in manpowerScore)
        {
            attackTargetsDictionary[pair.Key] -= manpowerScore[pair.Key] * cityManpowerScoreMultiplier;
            attackTargetsDictionary[pair.Key] -= distanceScore[pair.Key] * distanceScoreMultiplier;
        }



        foreach (KeyValuePair<UnitManager, float> cityPair in attackTargetsDictionary)
        {
            if (targetCity == null || cityPair.Value > attackTargetsDictionary[targetCity])
                targetCity = cityPair.Key;
        }

        if (targetCity) return targetCity;
        else
        {
            Debug.Log("Attack_DetermineTargetCity couldn't identify a target city");
            return null;
        }
    }
}
