using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Evaluators : MonoBehaviour
{
    public static AI_Evaluators ins;
    public List<UnitManager> cities;

    void Awake()
    {
        if (ins != null && ins != this) Destroy(this);
        else ins = this;
        ListCities();
    }

    public Dictionary<UnitManager, float> GetEnemyCityDistances(UnitManager friendlyCity, Dictionary<UnitManager, float> enemyCities)
    {
        Dictionary<UnitManager, float> enemyCityDistances = new Dictionary<UnitManager, float>();
        foreach (UnitManager city in enemyCities.Keys)
        {
            float distance = Vector3.Distance(friendlyCity.transform.position, city.transform.position);
            enemyCityDistances.Add(city, distance);
        }
        return enemyCityDistances;
    }

    public Dictionary<UnitManager, float> GetEnemyCitiesManpower(UnitManager originCity)
    {
        Dictionary<UnitManager, float> enemyCitiesManpower = new Dictionary<UnitManager, float>();
        
        foreach (UnitManager city in AI_Evaluators.ins.cities)
        {
            if (city.ownerFaction != originCity.ownerFaction && originCity.manPower > 1.1 * city.manPower)
            {
                enemyCitiesManpower.Add(city, city.manPower);
            }
        }

        return enemyCitiesManpower;
    }

    public UnitManager FindCityWithMostManpower(FactionModule faction)
    {
        UnitManager cityWithMostManpower = null;
        int highestManpower = 0;

        foreach (UnitManager city in faction.ownedCities)
        {
            if (city.manPower > highestManpower)
            {
                cityWithMostManpower = city;
                highestManpower = city.manPower;
            }
        }

        return cityWithMostManpower;
    }
    public UnitManager FindClosestCity(UnitManager city, bool onlyEnemyCities)
    {
        UnitManager closestCity = null;
        float closestCityDistance = 10000;
        float distance;
        for (int i = 0; i < cities.Count; i++)
        {
            if (!onlyEnemyCities || city.ownerFaction != cities[i].ownerFaction)
            {
                distance = Vector3.Distance(city.transform.position, cities[i].transform.position);
                if (closestCity == null || closestCityDistance > distance)
                {
                    closestCity = cities[i];
                    closestCityDistance = distance;
                }
            }
        }
        Debug.Log(closestCity.name);
        
        if (closestCity) return closestCity;
        else return null;
    }

    void ListCities()
    {
        UnitManager[] UMscripts = FindObjectsOfType<UnitManager>();

        for (int i = 0; i < UMscripts.Length; i++)
        {
            if (UMscripts[i].gameObject.tag == "City")
            {
                cities.Add(UMscripts[i]);
            }
        }
    }
}
