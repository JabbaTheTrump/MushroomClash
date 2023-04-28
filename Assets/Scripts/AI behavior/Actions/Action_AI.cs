using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action_AI : ScriptableObject
{
    public abstract void ExecuteAction(StateController stateController);
}
