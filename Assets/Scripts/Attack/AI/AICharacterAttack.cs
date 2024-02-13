using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICharacterAttack : BaseAttack
{

    public abstract void Set(Enemy character);

    public abstract void ColliderAttack(Character characterOponent, Character chatacter);

    public override void Attack(Vector3 dir, Vector3 StartPos, Character characterOponent = null, Character character = null) {}

    

}
