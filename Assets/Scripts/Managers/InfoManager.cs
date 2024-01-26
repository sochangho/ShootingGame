using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviourSingletonPersistent<InfoManager>
{

    

    public Test Info_Test { get; private set; } 
    public Table_Enemys TableEnemys { get; private set; }
    public Table_Stage TableStage { get; private set; }
    public Table_Player TablePlayer { get; private set; }
    public Table_Shot TableShot { get; private set; }



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

       TablePlayer = ResourceManager.LoadData<Table_Player>();
       TablePlayer.Set();

       TableShot = ResourceManager.LoadData<Table_Shot>();
       TableShot.Set();

    }

    public int GetCurrentPlayerCharacterID() 
    {
        return 1;
    }


}
