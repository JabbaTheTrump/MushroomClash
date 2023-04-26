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
    
    
    void Awake()
    {

    }

    void Start()
    {
        #region Initialize as marauders if faction is null
        if (ownerFaction == null) ownerFaction = StaticPropertyVariables.ins.MaraudersFaction;
        #endregion

        if (gameObject.tag == "City")
        {
            StartCoroutine(CalculateManpower());
        }

        if (ownerFaction.factionName != "marauders")
        {
            StartCoroutine(CalculateManpower());
        }

        #region Initialize as faction member
        ownerFaction.AddCity(this);
        #endregion

        #region GameEvent subbing
        #endregion

        //Display the initial manpower
        mpRenderer.text = $"MP: {manPower}";

        #region Unit naming
        unitName = NameGenerator.instance.GenerateName(gameObject.tag);
        gameObject.name = unitName;
        if (nameRenderer != null && unitName != null) nameRenderer.text = unitName;
        #endregion
    }

    void Update()
    {
        UpdateManpower();
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
    }

    public IEnumerator CalculateManpower()
    {
        for (; ;)
        {
            if(!isAttacked) manPower += growth;
            yield return new WaitForSeconds(StaticPropertyVariables.ins.secondsPerUpdate);
        }
    }

    public void UpdateManpower()
    {
        if (manPower < 0) manPower = 0;
        mpRenderer.text = $"MP: {manPower}";
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        MouseSelectSystem.instance.ProcessClick(eventData, this);
    }
}
