using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewResearch", menuName = "Research/New Research Node")]

public class ResearchNode : ScriptableObject
{
    public bool researched = false;
    public string researchName;
    public string researchDescription;
    public int researchCost;
    public Sprite researchSprite;
    public ResearchNode[] researchPrerequisites;

    public enum effect
    {
        gold,
        research,
        manpower,
        siege
    }

    public effect researchEffect;

    public float value;


}
