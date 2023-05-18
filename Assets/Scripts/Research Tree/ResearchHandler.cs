using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResearchHandler : MonoBehaviour
{
    public List<ResearchNode> unlockedResearch = new List<ResearchNode>();
    public ResearchNode currentResearch;
    public FactionModule faction;
    public int researchProgress;

    #region multipliers
    public float researchMultiplier = 1;
    public float goldGainMultiplier = 1;
    public float manpowerGainMultiplier = 1;
    public float siegeLevel = 1;
    #endregion

    public void StartResearch(ResearchNode researchData)
    {
        currentResearch = researchData;
        StartCoroutine(Research());
    }

    IEnumerator Research()
    {
        while (currentResearch.researchCost > researchProgress)
        {
            researchProgress += 1;
            yield return new WaitForSeconds(1);
        }
        if (researchProgress >= currentResearch.researchCost)
        {
            ResearchCompleted();
        }
        else
        {
            print($"There's an issue with the research {currentResearch.researchName} for the faction {faction.factionName}.");
        }
    }

    private void ResearchCompleted()
    {
        unlockedResearch.Add(currentResearch);
        ActivateResearch();
    }

    private void ActivateResearch()
    {
        ResearchNode.effect effect = currentResearch.researchEffect;
        if (effect == ResearchNode.effect.gold)
        {
            goldGainMultiplier += currentResearch.value;
        }
        else if (effect == ResearchNode.effect.research)
        {
            researchMultiplier += currentResearch.value;
        }
        else if (effect == ResearchNode.effect.manpower)
        {
            manpowerGainMultiplier += currentResearch.value;
        }
        else if (effect == ResearchNode.effect.siege)
        {
            siegeLevel = currentResearch.value;
        }
    }
}
