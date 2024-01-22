using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Vector3 Direction { get; private set; }

    private ObjectPooling objectPooling;

    override public void Created()
    { 
        base.Created();
        var data = InfoManager.Instance.TableEnemys.GetDataByName(CharacterName);
        CharaterInfo = new CharaterInfo(GameObjectType.Enemy, data.Id, data.Attack,data.Defence,data.Speed, data.Hp);

        objectPooling = GetComponent<ObjectPooling>();


    }

    override public void Destroyed() { base.Destroyed(); }

    override public void Active() 
    { 
        base.Active();

        var data = InfoManager.Instance.TableEnemys.GetData(CharaterInfo.Id);        
        CharaterInfo.RefreshCharacterData(data.Attack, data.Defence, data.Speed, data.Hp);
    }

    override public void InActive() { base.InActive(); }




    public override void CharacterUpdate()
    {
        // Direction = 방향가져오기

        transform.Translate(Direction * CharaterInfo.CharacterAbility.Speed * Time.deltaTime);
    }

    public override void DirectionUpdate(Vector3 direction)
    {
        
    }

    public override void Die()
    {
        //UnUse

        objectPooling.Destroy();
    }

}
