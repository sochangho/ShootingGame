using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIClosedAttack : AICharacterAttack
{
    private float distance;

    public override void Set(Enemy character)
    {
        distance = character.AttackDistance();
    }

    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent, Character character)
    {

        if (UtillMath.TwoPointClosed(characterOponent.transform.position, character.transform.position, distance))
        {

            var attack = character.characterInfo.Attack;

            characterOponent.Attacked(attack);
        }

    }


}
