using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// �÷��̾� ���Ÿ� ����
public class ShotAttack : BaseAttack
{
    protected CharacterInfo characterInfo;
    
       
    public ShotAttack(CharacterInfo characterInfo)
    {
        this.characterInfo = characterInfo;         
    }


    public override void Attack(Vector3 dir,Vector3 StartPos,Character characterOponent = null, Character character = null)
    {
       
      

       var go = GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Projectile, characterInfo.Shot_Id, StartPos);

       if(go == null)
        {
            return;
        }
       
       Projectile projectile = go.GetComponent<Projectile>();

       projectile.Fire(characterInfo, dir);

    }


}
