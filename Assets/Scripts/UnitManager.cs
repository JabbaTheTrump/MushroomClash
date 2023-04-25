using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitManager : MonoBehaviour, IPointerClickHandler
{
    public enum UnitTypes
    {
        Town,
        Mine,
        Forest,
        Army
    }

    public UnitTypes unitType;
    public SpriteRenderer unitSprite;
    public TextMesh nameRenderer;
    public TextMesh mpRenderer;
    public SpriteRenderer selectedGraphic;
    public FactionModule ownerFaction;
    public string unitName = "Unset";
    public int manPower = 0;
    public int growth = 1;
    public int goldIncome = 10;
    public bool isCapital = false;
    public bool isAttacked;
    
    
    void Start()
    {
        #region GameEvent subbing
        #endregion

        //Display the initial manpower
        mpRenderer.text = $"MP: {manPower}";

        #region Initialize as marauders if faction is null
        if (ownerFaction == null) ownerFaction = GameObject.Find("Marauders_Faction(Gaia)").GetComponent<FactionModule>();

        if (gameObject.tag == "City")
        {
            StartCoroutine(CalculateManpower());
        }
        #endregion

        #region Initialize as faction member
        ownerFaction.AddCity(this);
        #endregion

        #region Unit naming
        unitName = NameGenerator.instance.GenerateName(gameObject.tag);
        gameObject.name = unitName;
        if (nameRenderer != null && unitName != null) nameRenderer.text = unitName;
        #endregion
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

    public void CityCaptured(FactionModule attackingFaction)
    {
        ownerFaction = attackingFaction;
        isAttacked = false;
        StartCoroutine(CalculateManpower());

    }

    IEnumerator CalculateManpower()
    {
        if (ownerFaction.factionName != "marauders")
        {
            UpdateManpower();
            yield return new WaitForSeconds(1);
            //StaticPropertyVariables.instance.secondsPerUpdate - gotta update the timer
            if (!isAttacked)
            {
                manPower += growth;
                StartCoroutine(CalculateManpower());
            }
        }
    }

    public void UpdateManpower()
    {
        mpRenderer.text = $"MP: {manPower}";
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        MouseSelectSystem.instance.ProcessClick(eventData, this);
    }
}
