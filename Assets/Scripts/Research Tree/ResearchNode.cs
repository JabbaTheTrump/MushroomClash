using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNode : MonoBehaviour
{
    public bool researched = false;
    public string researchName;
    public string researchDescription;
    public int researchCost;
    public Sprite researchSprite;
    public ResearchNode[] researchPrequesits;

    public enum effect
    {
        gold,
        research,
        manpower,
        siege
    }

    public effect researchEffect;

    public float value;

    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = researchSprite;
        gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = researchName;
        gameObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = researchDescription;
        gameObject.transform.Find("CostBackground/Cost").GetComponent<TextMeshProUGUI>().text = researchCost.ToString();
    }

    public void StartResearch(FactionModule faction)
    {
        if (CheckIfCanResearch())
        {
            Console.WriteLine($"Researching {researchName}");
            faction.
        }
    }

    public bool CheckIfCanResearch()
    {
        if (researchPrequesits == null || researchPrequesits.Length == 0)
        {
            return true;
        }

        bool unlockedAllPrequesits = true;

        for (int i = 0; i < researchPrequesits.Length; i++)
        {
            if (!researchPrequesits[i].researched)
            {
                unlockedAllPrequesits = false;
            }
        }

        return unlockedAllPrequesits;
    }
}
