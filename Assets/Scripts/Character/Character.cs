using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class Character : BaseObject
{

    public CharaterInfo CharaterInfo { get;  private set; }

    public Attack attack;

    public event Action<float> AttactedEvent;  // attacked Event


    #region Override & Virtual
    override public void Created()
    {
        
        string objName = this.gameObject.name;        
        objName.Replace("(Clone)", "");

        //TODO : �����ʿ� ���̺� ���� ��  InfoManager ���� �� ����! 
        CharaterInfo = new CharaterInfo(objName, 10, 100, 2, 100);
        //

        attack = GetComponent<Attack>();

        if (attack != null)
        {
            attack.Set(CharaterInfo.CharacterAbility.Attack);
        }


        Debug.Log($"{CharaterInfo.CharacterName}  ����");

    }

    override public void Destroyed()
    {
        Debug.Log($"{CharaterInfo.CharacterName}  �ı�");

    }
    
    override public void Active(){

        Debug.Log($"{CharaterInfo.CharacterName}  Ȱ��");

    }

    override public void InActive(){

        Debug.Log($"{CharaterInfo.CharacterName}  ��Ȱ��");

    }


    virtual public float AttackedCalculate(float damage)
    {
        float curHp = CharaterInfo.CharacterStatus.HpCurrent - damage;

        if (curHp < 0)
        {
            CharaterInfo.CharacterStatus.HpCurrent = 0;
        }
        else 
        {
            CharaterInfo.CharacterStatus.HpCurrent = curHp;
        }

        return CharaterInfo.CharacterStatus.HpCurrent;
    }

    #endregion

    private void Update()
    {
        Move();
    }


    #region abstract

    abstract public void Move();

  
    #endregion



    #region public 

    public void Attacked(float damage)
    {
        float result =  AttackedCalculate(damage);
        AttactedEvent?.Invoke(result);
    }


    #endregion


}


