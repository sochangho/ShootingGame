using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraObject : BaseObject
{

    override public void Created(){}
    override public void Destroyed() { }
    override public void Active() { }
    override public void InActive() { }

    void Update()
    {
        CameraUpdate();
    }
    abstract public void CameraUpdate();
 
}
