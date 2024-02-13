using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroundCollisionAction : WeaponAction
{

    List<Enemy> enemies = new List<Enemy>(); 

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

  

    public override void ItemCollisionEnter(Collision collision)
    {
        base.ItemCollisionEnter(collision);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy == null)
        {


            return;
        }

        var find = enemies.Find(x => x.gameObject.GetInstanceID().Equals(collision.gameObject.GetInstanceID()));

        if (find == null)
        {
            ShowEffect(collision);
            enemies.Add(enemy);
            enemy.Attacked(value);
        }

    }

    public override void ItemCollisionExit(Collision collision)
    {
        base.ItemCollisionExit(collision);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy == null)
        {
            return;
        }

        var find = enemies.Find(x => x.gameObject.GetInstanceID().Equals(collision.gameObject.GetInstanceID()));

        if (find != null)
        {
            enemies.Remove(find);
        }

    }

    public void ShowEffect(Collision collsion)
    {
        ContactPoint contactPoint = collsion.contacts[0];

        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contactPoint.normal);

        var obj =  GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Effect, 3003, contactPoint.point);

        if(obj == null)
        {
            return;
        }

        obj.transform.rotation = rot;
    }


    public override void EndWave()
    {
        base.EndWave();        
        enemies.Clear();
    }

}
