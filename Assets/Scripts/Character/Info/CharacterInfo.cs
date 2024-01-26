using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInfo
{
    public int Id { get; protected set; }

    public string C_Name { get; protected set; }

    public float Attack { get; protected set; }
    public float Defence { get; protected set; }
    public float Speed { get; protected set; }
    public float Hp { get; protected set; }

   

    public int Shot_Id { get; protected set; }

    public float Shot_Speed { get; protected set; }
   
    public float Shot_Duration { get; protected set; }

    public float CurrHp;

    abstract public void SetInfo(object data);
    
    abstract public void RefreshInfo();



   
}



public class PlayerInfo : CharacterInfo
{
 
   
    public float Shot_LoadSpeed { get; protected set; }

    public int Shot_Count { get; protected set; }



    public override void SetInfo(object data)
    {
        Table_Player.CharacterPlayerInfo d = (Table_Player.CharacterPlayerInfo)data;

        this.Id = d.Id;
        this.C_Name = d.CharacterName;

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;
        this.Shot_Duration = d.Shot_Duration;
        this.Shot_LoadSpeed = d.Shot_LoadSpeed;
        this.Shot_Count = d.Shot_Count;
        this.Shot_Id = d.Shot_Id;

        this.Shot_Speed = d.Shot_Spped;

        CurrHp = Hp;

    }

    public override void RefreshInfo()
    {
        var d = InfoManager.Instance.TablePlayer.GetInfoById(this.Id);

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;
        this.Shot_Duration = d.Shot_Duration;
        this.Shot_LoadSpeed = d.Shot_LoadSpeed;
        this.Shot_Count = d.Shot_Count;
        this.Shot_Id = d.Shot_Id;
        this.Shot_Speed = d.Shot_Spped;
        CurrHp = Hp;

    }


}


public class EnemyInfo : CharacterInfo
{
    

    public override void SetInfo(object data)
    {
        Table_Enemys.EnemyData d = (Table_Enemys.EnemyData)data;

        this.Id = d.Id;
        this.C_Name = d.Name;

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;

        //this.Shot_Id = d.Shot_Id;

        //this.Shot_Duration = d.Shot_Duration;

        //this.Shot_Speed = d.Shot_Spped;

        CurrHp = Hp;

    }


    public override void RefreshInfo()
    {
        var d = InfoManager.Instance.TableEnemys.GetData(Id);

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;


        //this.Shot_Id = d.Shot_Id;

        //this.Shot_Duration = d.Shot_Duration;

        //this.Shot_Speed = d.Shot_Spped;

        CurrHp = Hp;
    }


}