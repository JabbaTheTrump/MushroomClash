using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public FactionModule factionModule;
    public State currentState;

    private void Update()
    {
        currentState.CheckConditions(this);
    }
}
