using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundColltionDefence : WeaponAction
{
    public override void MoveWeapon()
    {

        Vector3 pos = player.transform.position;

        pos.y += 0.5f;


        UtillMath.RotateAroundDistanceLookAt(weaponObjectTransform, pos,
            Vector3.up, diff, speed, ref angle);

    }

    public override void Reposition(float reangle)
    {
        Vector3 pos = player.transform.position;

        pos.y += 0.5f;

        angle = reangle;

        UtillMath.RotateAroundDistanceInit(weaponObjectTransform, pos,
            Vector3.up, diff, angle);

    }

    public override void ItemTriggerEnter(Collider other)
    {

        base.ItemTriggerEnter(other);

        Projectile pj = other.gameObject.GetComponent<Projectile>();

        if (pj != null && pj.GetSubjectTypeProjectile() == GameObjectType.Enemy )
        {
            thisItem.WaveEnd();
            pj.UnUseProjectile();
           
        }

    }


    public override void ItemTriggerExit(Collider collider)
    {

        base.ItemTriggerExit(collider);


    }
}
