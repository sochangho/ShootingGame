using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���� ���� ĳ�� �뵵
//�÷��̾ �ٽ� ������ �� ������ ���
public class CharaterInfo
{
    public string CharacterName { get; private set; }

    public CharacterAbility CharacterAbility;

    public CharacterStatus CharacterStatus;

    public CharaterInfo(string name, float attack , float defence, float speed, float hp)
    {
        CharacterName = name;

        CharacterAbility = new CharacterAbility()
        {
            Attack = attack,
            Defence = defence,
            Speed = speed,
        };

        CharacterStatus = new CharacterStatus()
        {
            HpTotal = hp,
            HpCurrent = hp,
        };
    }
     

}


public struct CharacterAbility
{
    public float Attack;
    public float Defence;
    public float Speed;
}



public struct CharacterStatus
{
    public float HpTotal;
    public float HpCurrent;
}
