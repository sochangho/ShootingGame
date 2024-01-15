using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//정보 저장 캐싱 용도
//플레이어가 다시 생성될 때 가져와 사용
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
