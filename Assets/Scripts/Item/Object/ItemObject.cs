using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : BaseObject
{
    public override void Created()
    {
        
    }

    public override void Active()
    {
        
    }

    public override void InActive()
    {
        
    }

    public override void Destroyed()
    {
        
    }



    public virtual void ItemTriggerEnter(Collider other)
    {

    }

    public virtual void ItemTriggerExit(Collider other)
    {

    }

    public virtual void ItemCollisionEnter(Collision collision) { } 

    public virtual void ItemCollisionExit(Collision collision) { }

    public virtual void EndWaveItemObject() { }

    private void OnTriggerEnter(Collider other)
    {
        ItemTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ItemTriggerExit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ItemCollisionEnter(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        ItemCollisionExit(collision);
    }



}
