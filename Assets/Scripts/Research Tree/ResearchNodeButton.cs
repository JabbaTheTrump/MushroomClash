using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResearchNodeButton : MonoBehaviour
{
    public ResearchNode research;
    public bool researched = false;


    public Image uiIcon;
    public TextMeshProUGUI uiName;
    public TextMeshProUGUI uiDescription;
    public TextMeshProUGUI uiCost;

    /*gameObject.GetComponent<Image>().sprite = researchSprite;
gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = researchName;
gameObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = researchDescription;
gameObject.transform.Find("CostBackground/Cost").GetComponent<TextMeshProUGUI>().text = researchCost.ToString();*/

    void Start()
    {
        uiName.text = research.name;
        uiDescription.text = research.researchDescription;
        uiCost.text = research.researchCost.ToString();
        uiIcon.sprite = research.researchSprite;
    }

    void Update()
    {
        uiCost.text = research.researchCost.ToString();
    }
}
