using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack
{
    abstract public void Attack(Vector3 dir, Vector3 StartPos,Character characterOponent = null, Character character = null);


}
