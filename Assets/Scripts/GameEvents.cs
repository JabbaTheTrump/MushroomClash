using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    //public static GameEvents current;
    public UnityEvent sendArmyToCity;
    
    void Awake()
    {
        //current = this;
        
    }
}
