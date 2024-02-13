using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtillPhysics
{   
    public static void EnemyToPlayerRaycast(Character enemy, Character player,System.Action<RaycastHit> action)
    {

        var direction = player.transform.position - enemy.transform.position;

        direction = direction.normalized;

        var startPos = enemy.transform.position;
        startPos.y += 0.5f;

        var hits = Physics.RaycastAll(startPos, direction, 30f);

        if (hits != null)
        {
            foreach (var h in hits)
            {
                if (h.collider.gameObject.tag == "Player")
                {
                    action?.Invoke(h);

                }
            }
        }
    }



}
