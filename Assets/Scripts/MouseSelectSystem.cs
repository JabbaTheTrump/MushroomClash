using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseSelectSystem : MonoBehaviour
{
    public ArmyCreationSlider sliderScript;
    public GameObject armyTemplate;
    public static MouseSelectSystem instance;
    public UnitManager selectedItem;
    public UnitManager orderTarget;
    public string state;
    public bool controlsEnabled = true;

    private void Start()
    {
        instance = this;
        Cursor.visible = true;
    }

    void OnLeftClick()
    {
        if (controlsEnabled)
        {
            TestForDeselect();
        }
    }

    public void SelectObject(UnitManager selectedItem)
    {

        this.selectedItem = selectedItem;
        selectedItem.selectedGraphic.enabled = true;
    }

    public void GiveOrder(UnitManager orderTarget)
    {
        this.orderTarget = orderTarget;

        if (selectedItem != orderTarget)
        {
            if (orderTarget.gameObject.tag == "City")
            {
                sliderScript.transform.parent.gameObject.SetActive(true);
                sliderScript.InitiateSlider(selectedItem);
            }
        }
    }



    public void CreateArmy(int manPower)
    {
        Vector3 spawnPoint;
        
        spawnPoint = selectedItem.transform.position;
        GameObject army = Instantiate(armyTemplate,spawnPoint, new Quaternion(0,0,0,0));
        ArmyMovement armyMovementScript = army.GetComponent<ArmyMovement>();
        UnitManager armyUnitManager = army.GetComponent<UnitManager>();

        armyUnitManager.manPower = manPower;
        armyUnitManager.owner = selectedItem.owner;
        armyMovementScript.MoveToPosition(orderTarget.gameObject);
    }

    public void ProcessClick(PointerEventData eventData, UnitManager requestingManager)
    {
        if (controlsEnabled)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                SelectObject(requestingManager);
                Debug.Log("city selected: " + requestingManager.unitName);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (selectedItem != null && selectedItem.owner == "player")
                {
                    orderTarget = null;
                    GiveOrder(requestingManager);
                }
            }
        }
    }

        void TestForDeselect()
    {
        if (selectedItem != null)
        {
            selectedItem.selectedGraphic.enabled = false;
            selectedItem = null;
        }
    }
}
