using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDashAttack : AICharacterAttack
{

    private Enemy enemy;

    private bool attacking; //공격중...

    private Vector3 destiantionPos; // 데쉬 공격 목적지 

    private Vector3 direction; // 방향

    public override void Set(Enemy character)
    {
        enemy = character;

        enemy.EventAttack += DashAttackAIState;

        enemy.EventCollider += ColliderAttack;
       
    }

    public override void ColliderAttack(Character characterOponent, Character chatacter)
    {
        if (attacking)
        {
            Debug.Log($"<color=green>  Collider Attack </color>");
            Attack(Vector3.zero, Vector3.zero, characterOponent, chatacter);        
        }
    }



    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent = null, Character character = null)
    {        
       
            characterOponent.Attacked(character.characterInfo.Attack);       
    }


    private void DashAttackAIState(Character characterOponent, Character character)
    {
        if (!attacking)
        {
            destiantionPos = characterOponent.transform.position;

            Vector3 dir = destiantionPos - character.transform.position;

            direction = dir.normalized;

            attacking = true;

            character.transform.LookAt(characterOponent.transform);
        }

         

        if (!UtillMath.TwoPointClosed(destiantionPos , character.transform.position, 0.4f ))
        {            
           character.transform.position = Vector3.MoveTowards(character.transform.position, destiantionPos, character.characterInfo.Speed * 10 * Time.deltaTime);           
        }
        else
        {            
            attacking = false;
            enemy.AIRefresh();
        }

        
    }

}
