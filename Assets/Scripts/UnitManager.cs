using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitManager : MonoBehaviour, IPointerClickHandler
{
    public TextMesh nameRenderer;
    public TextMesh mpRenderer;
    public SpriteRenderer selectedGraphic;
    public string unitName = "Unset";
    public string owner = "Unset";
    public int manPower = 0;
    public int growth = 1;
    public bool isCapital = false;
    public bool isAttacked;

    void Start()
    {
        mpRenderer.text = $"MP: {manPower}";
        if (owner != "Unset")
        {
            StartCoroutine(CalculateManpower());
        }
        
        unitName = NameGenerator.instance.GenerateName(gameObject.tag);
        if (nameRenderer != null && unitName != null) nameRenderer.text = unitName;
        if (gameObject.tag == "City") gameObject.name = $"City: {unitName}";
        else if (gameObject.tag == "Army") gameObject.name = $"Army: {unitName}";
        else 
        {
            Debug.Log($"cannot find tag for {unitName}");
        }
    }

    void Update()
    {
    }

    public UnitManager(string unitName, string owner, int manPower, int spareManpower)
    {
        this.unitName = unitName;
        this.owner = owner;
        this.manPower = spareManpower;
    }

    IEnumerator CalculateManpower()
    {
        mpRenderer.text = $"MP: {manPower}";
        yield return new WaitForSeconds(1);
        //StaticPropertyVariables.instance.secondsPerUpdate - gotta update the timer
        if (!isAttacked)
        {
            manPower += growth;
        }
        StartCoroutine(CalculateManpower());
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        MouseSelectSystem.instance.ProcessClick(eventData, this);
    }
}
