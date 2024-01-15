using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAttack : Attack
{
    private void OnTriggerEnter(Collider other)
    {
        AttackCharacter(other.gameObject);
    }

}
