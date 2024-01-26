using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttacked : ISubject 
{
    private CharacterInfo characterInfo;

    private List<IObserver> observers = new List<IObserver>();
 
    public BaseAttacked(CharacterInfo characterInfo)
    {
        this.characterInfo = characterInfo;
    }

    virtual public void Attacked(float attack)
    {
        float defence = characterInfo.Defence;

        float damage = UtillMath.DamageCalculate(attack, defence);

        if (characterInfo.CurrHp - damage > 0)
        {
            characterInfo.CurrHp -= damage;

            Debug.Log($"<color=green>Name: {characterInfo.C_Name } , HP : {characterInfo.CurrHp}/{characterInfo.Hp}</color>");
        }
        else
        {
            characterInfo.CurrHp = 0;

            Debug.Log($"<color=green>Name: {characterInfo.C_Name} // Die </color>");
        }

        NotifyObserver();

    }

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
        foreach(var o in observers)
        {
            o.UpdataData(characterInfo.CurrHp);
        }

    }



}
