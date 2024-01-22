using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviourSingletonPersistent<InfoManager>
{
    public Test Info_Test { get; private set; } 
    public Table_Enemys TableEnemys { get; private set; }

    public Table_Stage TableStage { get; private set; }

    public override void Awake()
    {
        base.Awake();
        LoadDatas();
    }

    public void LoadDatas()
    {
       //-----테스트 용도---------------------------
       Info_Test = ResourceManager.LoadData<Test>();
       Info_Test.Set();

       //------------------------------------------- 

       TableEnemys = ResourceManager.LoadData<Table_Enemys>();
       TableEnemys.Set();

       TableStage = ResourceManager.LoadData<Table_Stage>();
       TableStage.Set();

       
    }



}
