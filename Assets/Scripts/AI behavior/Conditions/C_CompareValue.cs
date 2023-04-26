using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Condition/Compare Values")]
public class C_CompareValue : Conditions
{
    public enum value
    {
        gold,
        iron
    }

    [InspectorName("Value")] public value valE;


    public enum operation
    {
        isBiggerThan,
        isSmallerThan,
        EqualsTo,
    }

    [InspectorName("Operator")]  public operation opeE;

    [SerializeField] public float valueToCompareTo;

    public override bool ConditionAchieved(StateController controller)
    {
        float valueToCompare = 0;
        
        switch(valE)
        {
            case value.gold:
                valueToCompare = controller.factionModule.gold;
                break;
        }

        switch (opeE)
        {
            case operation.EqualsTo:
                return (valueToCompare == valueToCompareTo);

            case operation.isBiggerThan:
                return (valueToCompare > valueToCompareTo);

            case operation.isSmallerThan:
                return (valueToCompare < valueToCompareTo);
        }

        Debug.Log($"Condition CompareValue for {controller} has failed.");
        return true;
    }
}
