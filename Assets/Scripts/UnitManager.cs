using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitManager : MonoBehaviour, IPointerClickHandler
{
    public FactionModule maraudersFaction;
    public SpriteRenderer unitSprite;
    public TextMesh nameRenderer;
    public TextMesh mpRenderer;
    public SpriteRenderer selectedGraphic;
    public FactionModule ownerFaction;
    public string unitName = "Unset";
    public int manPower = 0;
    public int growth = 1;
    public bool isCapital = false;
    public bool isAttacked;

    void Start()
    {
        maraudersFaction = GameObject.Find("Marauders_Faction(Gaia)").GetComponent<FactionModule>();   
        mpRenderer.text = $"MP: {manPower}";
        if (ownerFaction == null) ownerFaction = maraudersFaction;
        if (gameObject.tag == "City")
        {
            StartCoroutine(CalculateManpower());
            unitSprite.color = ownerFaction.factionColor;
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

    public UnitManager(string unitName, FactionModule owner, int manPower, int spareManpower)
    {
        this.unitName = unitName;
        this.ownerFaction = owner;
        this.manPower = spareManpower;
    }

    public void CityCaptured(object sender, GameEvents.OnCityCaptureEventArgs eArgs)
    {
        if (eArgs.capturedCity == this)
        {
            ownerFaction = eArgs.attacker.ownerFaction;
            isAttacked = false;
            unitSprite.color = ownerFaction.factionColor;
            StartCoroutine(CalculateManpower());
        }
    }

    IEnumerator CalculateManpower()
    {
        if (ownerFaction != maraudersFaction)
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
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        MouseSelectSystem.instance.ProcessClick(eventData, this);
    }
}
