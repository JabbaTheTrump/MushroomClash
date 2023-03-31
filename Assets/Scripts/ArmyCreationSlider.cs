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
    private UnitManager city;
    
    void Start()
    {
        sliderComponent.onValueChanged.AddListener(delegate{ValueChangeCheck();});
    }

    public void InitiateSlider(UnitManager city)
    {
        this.city = city;
        MouseSelectSystem.instance.controlsEnabled = false;
        maxManpower = city.manPower;
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
            city.manPower -= (int) sliderComponent.value;
            MouseSelectSystem.instance.CreateArmy((int) sliderComponent.value);
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
