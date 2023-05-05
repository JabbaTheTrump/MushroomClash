using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ui_Elements : MonoBehaviour
{
    public FactionModule playerFaction;
    public TextMeshProUGUI totalGold;
    public TextMeshProUGUI goldIncome;

    private void Update()
    {
        UpdateGoldElements();
    }

    public void UpdateGoldElements()
    {
        totalGold.text = playerFaction.gold.ToString();
        goldIncome.text = "+" + playerFaction.goldIncome.ToString();
    }
}
