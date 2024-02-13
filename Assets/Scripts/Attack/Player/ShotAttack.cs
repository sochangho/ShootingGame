using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// 플레이어 원거리 공격
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

        var info = InfoManager.Instance.TableShot.GetInfoById(characterInfo.Shot_Id);

       projectile.FlashId = info.Flash;
       projectile.HitId = info.Hit; 

       projectile.Fire(characterInfo, dir);

    }


}
