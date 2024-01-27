using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = PathString.TABLE)]
public class Table_Enemys : ScriptableObject,IData
{
    [System.Serializable]
    public class EnemyData
    {
        public int Id;
        
        public string Name;

        public float Attack;
        
        public float Defence;
        
        public float Speed;
        
        public float Hp;

        public int Capacity;


        public int Shot_Id;

        public float Shot_Duration;

        public float Shot_Spped;

        public string AIAttackClass;



    }

    public List<EnemyData> Enemys; // Replace 'EntityType' to an actual type that is serializable.

    public Dictionary<int, EnemyData> data { get; private set; }


    public void Set()
    {
        data = new Dictionary<int, EnemyData>();

        foreach (var e in Enemys)
        {
            data.Add(e.Id, e);
        }
    }

    public void Reales()
    {

    }

    public EnemyData GetData(int id)
    {
       return data[id];
    }

    public EnemyData GetDataByName(string name)
    {        
       var data = Enemys.Find(x => string.Equals(x.Name,name));
       return data;
    }
}
