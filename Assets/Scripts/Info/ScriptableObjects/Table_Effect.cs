using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Effect : ScriptableObject, IData
{
	[System.Serializable]
	public class EffectInfo
    {
		public int Id; 
		public string Name;
        public int Capacity;


    }


    public List<EffectInfo> Shots; // Replace 'EntityType' to an actual type that is serializable.


    private Dictionary<int, EffectInfo> dic_Shots;

    private bool isLoaded = false;

    public void Set()
    {
        if (isLoaded)
        {
            return;
        }

        isLoaded = true;


        dic_Shots = new Dictionary<int, EffectInfo>();

        for (int i = 0; i < Shots.Count; ++i)
        {
            dic_Shots.Add(Shots[i].Id, Shots[i]);
        }
    }

    public void Reales()
    {


    }

    public EffectInfo GetInfoById(int id)
    {
        if (dic_Shots.ContainsKey(id))
        {
            return dic_Shots[id];
        }

        return null;
    }


}
