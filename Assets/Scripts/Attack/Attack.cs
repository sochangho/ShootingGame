using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float Damage { get; private set; }


    public void Set(float damage)
    {
        Damage = damage;
    }


    public void AttackCharacter(GameObject go)
    {
       Character character =  go.GetComponent<Character>();
       character.Attacked(Damage);
    }    


}
