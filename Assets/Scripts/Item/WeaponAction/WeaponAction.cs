using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAction
{
    protected float value;

    protected float speed = 240f;
    
    protected Player player;

    protected Vector3 diff = new Vector3(2,0,0);

    protected float angle = 0;

    protected bool isCollider = false;

    protected Transform weaponObjectTransform;

    protected Equipment thisItem;

   public void Init(float value, Player player, Transform ot,Equipment item)
   {
        this.value = value;
        this.player = player;
        this.weaponObjectTransform = ot;
        this.thisItem = item;
   }
    
   public virtual void MoveWeapon() { }
   
   public virtual void Reposition(float reangle) { }

   public virtual void ItemTriggerEnter(Collider collider) { }
   
   public virtual void ItemTriggerExit(Collider collider) { }

   public virtual void ItemCollisionEnter(Collision collision) { }

   public virtual void ItemCollisionExit(Collision collision) { }

    public virtual void EndWave() { }


}
