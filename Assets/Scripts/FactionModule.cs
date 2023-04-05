using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionModule : MonoBehaviour
{
    public int gold;
    public string factionName;
    public UnitManager capital;
    public UnitManager[] ownedCities;
    public Color factionColor;

    private void Awake() {
        AssignFactionColor();
    }

    private void AssignFactionColor()
    {
        if (factionName == "player") factionColor = Color.blue;
        else if (factionName == "marauders") factionColor = Color.gray;
    }
}
