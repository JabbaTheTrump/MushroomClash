using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmyCreationSlider : MonoBehaviour
{
    public Slider sliderComponent;
    public TextMeshProUGUI manPowerText;
    public int maxManpower;
    private UnitManager originCity;
    private UnitManager targetCity;
    
    void Start()
    {
        sliderComponent.onValueChanged.AddListener(delegate{ValueChangeCheck();});
    }

    public void InitiateSlider(UnitManager originCity, UnitManager targetCity)
    {
        Debug.Log("opening slider");
        this.originCity = originCity;
        this.targetCity = targetCity;
        MouseSelectSystem.instance.controlsEnabled = false;
        maxManpower = originCity.manPower;
        sliderComponent.maxValue = maxManpower;
    }

    public void ValueChangeCheck()
    {
        manPowerText.text = "" + sliderComponent.value;
    }

    public void SendArmy()
    {
        if (sliderComponent.value > 0)
        {
            ArmyCreation.instance.CreateArmy((int) sliderComponent.value, originCity, targetCity);
            RemoveSlider();
        }
    }

    public void RemoveSlider()
    {
        MouseSelectSystem.instance.controlsEnabled = true;
        sliderComponent.value = 0;
        transform.parent.gameObject.SetActive(false);
    }
}
