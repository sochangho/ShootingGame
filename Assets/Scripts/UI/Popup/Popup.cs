using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class Popup : BaseObject
{
    public Type type; 

    override public void Created(){}
    override public void Destroyed(){}
    override public void Active(){ }
    override public void InActive(){}



    virtual public void Open(UnityAction action = null) 
    {
        //action?.Invoke();

        this.gameObject.SetActive(true);
        
        gameObject.transform.SetAsLastSibling();
    }

    virtual public void Close(UnityAction action = null)
    {
        //action?.Invoke();

        this.gameObject.SetActive(false);    
    }

}
