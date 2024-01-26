using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Shot : ScriptableObject ,IData
{

    [System.Serializable]
	public class ShotInfo
    {

        public int Id;

        public float Shot_Attack;
        
        public string Shot_Name;

        public int Shot_Capacity;
    }

    public List<ShotInfo> Shots; // Replace 'EntityType' to an actual type that is serializable.

    private Dictionary<int, ShotInfo> dic_Shots;

    public void Set()
    {
        dic_Shots = new Dictionary<int, ShotInfo>();

        for(int i = 0; i < Shots.Count; ++i)
        {
            dic_Shots.Add(Shots[i].Id, Shots[i]);
        }
    }

    public void Reales()
    {


    }

    public ShotInfo GetInfoById(int id)
    {
        if (dic_Shots.ContainsKey(id))
        {
            return dic_Shots[id];
        }

        return null;
    }

}
