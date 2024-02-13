using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterInfo : ISubject
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

    private List<IObserver> observers = new List<IObserver>();


    public void ResisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }



    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }


    public void NotifyObserver()
    {
        foreach (var o in observers)
        {            
            o.UpdateData(1.0f);
        }

    }


}



public class PlayerInfo : CharacterInfo 
{

    private int count;
    
    public float Shot_LoadSpeed { get; protected set; }

    public int Shot_Count { get { return count; }
        protected set {

            count = value;

            //Debug.Log($"<color=magenta> PlayerInfo ÃÑ¾Ë °³¼ö {count} </color>");
        
        
        } }

    protected int shotCountOrigin;


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

        shotCountOrigin = d.Shot_Count;
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


        NotifyObserver();
    }

    public bool ShotDecrease()
    {
        Shot_Count--;

        Debug.Log($"<color=magenta> ÃÑ¾Ë °³¼ö {Shot_Count} <color>");

        if (Shot_Count > 0)
        {            
            return true;
        }
        else
        {
            Shot_Count = 0;
        }

       
        return false;
    }

    public void ShotCharge()
    {
        
        Shot_Count = shotCountOrigin;
        Debug.Log($"<color=magenta> ÃæÀü!!!!!!!! {Shot_Count} <color>");
    }

    public void PlayerAbilitySet(PlayerAbilityValue value)
    {
        Attack = value.Attack;
        Defence = value.Defence;
        Speed = value.Speed;
        Shot_Duration = value.Shot_Duration;
        Shot_LoadSpeed = value.Shot_LoadSpeed;
        Shot_Speed = value.Shot_Speed;
        //Shot_Count = value.Shot_Count;
    }

}


public class EnemyInfo : CharacterInfo 
{
    public string AIAttackType { get; protected set; }

    public override void SetInfo(object data)
    {
        Table_Enemys.EnemyData d = (Table_Enemys.EnemyData)data;

        this.Id = d.Id;
        this.C_Name = d.Name;

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;

        this.Shot_Id = d.Shot_Id;

        this.Shot_Duration = d.Shot_Duration;

        this.Shot_Speed = d.Shot_Spped;

        AIAttackType = d.AIAttackClass;

        CurrHp = Hp;

    }


    public override void RefreshInfo()
    {
        var d = InfoManager.Instance.TableEnemys.GetData(Id);

        this.Attack = d.Attack;
        this.Defence = d.Defence;
        this.Speed = d.Speed;
        this.Hp = d.Hp;


        this.Shot_Id = d.Shot_Id;

        this.Shot_Duration = d.Shot_Duration;

        this.Shot_Speed = d.Shot_Spped;

        CurrHp = Hp;


        NotifyObserver();
    }


}



public struct PlayerAbilityValue
{
    public float Attack;
    public float Defence;
    public float Speed;
    public float Shot_Duration;
    public float Shot_LoadSpeed;
    public float Shot_Speed;
    public int Shot_Count;

}
