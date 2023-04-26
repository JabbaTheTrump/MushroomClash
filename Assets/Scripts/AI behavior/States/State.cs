using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject
{
    [Expandable] public Conditions[] conditions;

    public bool CheckConditions(StateController controller)
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].ConditionAchieved(controller))
            {
                Debug.Log("false");
                return false;
            }
        }
        Debug.Log("true");
        return true;
    }
}
