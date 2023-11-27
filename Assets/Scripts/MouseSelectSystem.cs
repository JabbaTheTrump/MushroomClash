using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseSelectSystem : MonoBehaviour
{
    public ArmyCreationSlider sliderScript;
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
        if (selectedItem.selectedGraphic != null) selectedItem.selectedGraphic.enabled = true;
    }

    public void GiveOrder(UnitManager orderTarget)
    {
        this.orderTarget = orderTarget;

        if (selectedItem != orderTarget)
        {
            sliderScript.transform.parent.gameObject.SetActive(true);
            sliderScript.InitiateSlider(selectedItem, orderTarget);
        }
    }

    public void ProcessClick(PointerEventData eventData, UnitManager requestingManager)
    {
        if (controlsEnabled)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("left click");
                SelectObject(requestingManager);
                Debug.Log("city selected: " + requestingManager.unitName);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                Console.WriteLine("right click");
                if (selectedItem != null && selectedItem.ownerFaction.factionName == "player")
                {
                    orderTarget = null;
                    GiveOrder(requestingManager);
                }
            }

            else
                Console.WriteLine("void click");
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
