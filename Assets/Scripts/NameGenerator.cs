using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    public static NameGenerator instance;
    public List<string> existingCities;
    public List<string> cityName;
    public List<string> armyTitle;
    public List<string> armyType;
    public List<string> cityPrefix;
    public List<string> citySuffix;
    private int armyNumber;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
    }
    
    void Update()
    {
    }

    public string GenerateName(string tag)
    {
        string generatedName;
        if (tag == "City")
        {
            generatedName = GenerateCityName();
            existingCities.Add(generatedName);
            Debug.Log(generatedName);
            return generatedName;
        }
        else if (tag == "Army")
        {
            return GenerateArmyName();
        }
        Debug.Log("No matching tag check detected for " + tag);
        return "error, wrong tag";
    }

    string GenerateCityName()
    {
        string prefix = cityPrefix[Random.Range(0,cityPrefix.Count)];
        string suffix = citySuffix[Random.Range(0,citySuffix.Count)];
        string name = $"{prefix}{suffix}";
        if (DoesNameExist(name,existingCities))
        {
            return GenerateCityName();
        }
        else
        {
            return name;
        }
    }

    private bool DoesNameExist(string currentName, List<string> currentList)
    {
        for (int i = 0; currentList.Count > i; i++)
        {
            if (currentName == currentList[i])
            {
                return true;
            }
        }
        return false;
    }

    string GenerateArmyName()
    {
        string title = armyTitle[Random.Range(0,armyTitle.Count)];
        string type = armyType[Random.Range(0,armyType.Count)];
        armyNumber = Random.Range(1,100);
        string numberString = armyNumber.ToString();
        string numberSuffix = "th";
        char lastNumber = numberString[numberString.Length - 1];
        switch(lastNumber)
        {
            case '1':
            numberSuffix = "st";
            break;
            case '2':
            numberSuffix = "nd";
            break;
            case '3':
            numberSuffix = "rd";
            break;
        }

        string name = $"{armyNumber}{numberSuffix} {title} {type}";
        Debug.Log(name);
        return name;
    }
}
