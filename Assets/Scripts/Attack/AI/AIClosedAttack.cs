using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIClosedAttack : AICharacterAttack
{
    private float distance;

    private int layerMask;

    public override void Set(Enemy character)
    {
        distance = character.AttackDistance();

        layerMask = 1 << 9;
    }

    public override void ColliderAttack(Character characterOponent, Character chatacter)
    {
        
    }

    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent, Character character)
    {
       

        if (UtillMath.TwoPointClosed(characterOponent.transform.position, character.transform.position, distance))
        {

       
            UtillPhysics.EnemyToPlayerRaycast(character, characterOponent, (h) =>
             {
                 GameScene.Instance.ObjectPoolManager
                            .UseObject(GameObjectType.Effect, 3002, h.point);

             });

            var attack = character.characterInfo.Attack;

            characterOponent.Attacked(attack);
        }

    }


}
