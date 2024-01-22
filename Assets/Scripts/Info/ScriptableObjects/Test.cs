using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Test : ScriptableObject, IData
{
	public List<TestEntity> Sheet1; // Replace 'EntityType' to an actual type that is serializable.

    public Dictionary<int, TestEntity> data { get; private set; }


	public void Set()
    {
        data = new Dictionary<int, TestEntity>();

        foreach(var e in Sheet1)
        {            
           data.Add(e.id, e);
        }
    }

    public void Reales()
    {

    }

}
