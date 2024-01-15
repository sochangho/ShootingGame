using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Character : BaseObject
{

    public CharaterInfo CharaterInfo { get;  private set; }

    public event Action<float> AttactedEvent;  // UI Event

    override public void Created(){}

    override public void Destroyed(){}
    
    override public void Active(){}

    override public void InActive(){}


    private void Update()
    {
        Move();
    }

    abstract public void Move();
   
}


