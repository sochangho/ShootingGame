using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
    public int instanceid { get; protected set; }

    public int Item_Id { get; protected set; }

    public string Item_Name { get; protected set; }

    abstract public void UseItem();

    abstract public void UnUseItem();

}
