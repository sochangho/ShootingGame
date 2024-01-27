using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDashAttack : AICharacterAttack
{

    private Enemy enemy;

    private bool attacking; //������...

    private Vector3 destiantionPos; // ���� ���� ������ 

    private Vector3 direction; // ����

    public override void Set(Enemy character)
    {
        enemy = character;

        enemy.EventAttack += DashAttackAIState;

       
    }

    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent = null, Character character = null)
    {
        if(UtillMath.TwoPointClosed(characterOponent.transform.position, character.transform.position, 0.4f))
        {
            characterOponent.Attacked(character.characterInfo.Attack);
        }

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

            Attack(Vector3.zero, Vector3.zero, characterOponent, character);
            attacking = false;
            enemy.AIRefresh();
        }

        
    }

}