using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyCreation : MonoBehaviour
{
    public static ArmyCreation instance;
    public GameObject armyTemplate;


    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }


    public void CreateArmy(int manPower, UnitManager originCity, UnitManager targetCity)
    {
        Vector3 spawnPoint;
        
        spawnPoint = originCity.transform.position;
        GameObject army = Instantiate(armyTemplate,spawnPoint, new Quaternion(0,0,0,0));
        ArmyMovement armyMovementScript = army.GetComponent<ArmyMovement>();
        UnitManager armyUnitManager = army.GetComponent<UnitManager>();

        armyUnitManager.manPower = manPower;
        armyUnitManager.ownerFaction = originCity.ownerFaction;
        armyMovementScript.MoveToPosition(targetCity.gameObject);
        originCity.manPower -= manPower;
    }
}
