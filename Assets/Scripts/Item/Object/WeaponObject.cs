using UnityEngine;

public class WeaponObject : ItemObject
{
    private Item item; 

    private WeaponAction weaponAction;

    public void Init(float value, Player player,string weaponActionName, Equipment item)
    {
        this.item = item;
        weaponAction = ClassFactory.GetClassInstanceWeaponActon(weaponActionName);
        weaponAction.Init(value, player, this.transform,item);
    }

    public void MoveWeapon()
    {
        weaponAction.MoveWeapon();
    }

    public void Reposiotion(float angle)
    {
        weaponAction.Reposition(angle);
    }


    public override void ItemTriggerEnter(Collider other)
    {
        weaponAction.ItemTriggerEnter(other);              
    }

    public override void ItemTriggerExit(Collider other)
    {
        weaponAction.ItemTriggerExit(other);
    }

    public override void ItemCollisionEnter(Collision collision)
    {
        weaponAction.ItemCollisionEnter(collision);
    }

    public override void ItemCollisionExit(Collision collision)
    {
        weaponAction.ItemCollisionExit(collision);
    }

    

    public override void EndWaveItemObject()
    {
        weaponAction.EndWave();
    }
}
