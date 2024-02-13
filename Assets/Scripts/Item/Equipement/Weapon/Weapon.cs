using UnityEngine;

public class Weapon : Equipment
{
    protected Table_Item.ItemWeaponInfo itemWeaponInfo;

    protected WeaponObject weaponObject;

    public  Weapon(Table_Item.ItemWeaponInfo itemWeaponInfo)
    {
        this.itemWeaponInfo = itemWeaponInfo;

        Item_Id = itemWeaponInfo.Id;
        Item_Name = itemWeaponInfo.Name;
        Count = itemWeaponInfo.Duration;
    }

    public void InstantiateWeapon()
    {
        var w =  ResourceManager.Load<WeaponObject>(AssetType.Prefab , $"{PathString.PREFABS_ITEM_WEAPON}/{Item_Name}");

        var clone =  GameObject.Instantiate(w);

        weaponObject = clone;

        weaponObject.Init(itemWeaponInfo.Value, GameScene.Instance.GameRoopController.player, itemWeaponInfo.WeaponActionClass,this);
    }

    public void WeaponReposiotion(float angle)
    {
        if(weaponObject == null)
        {
            InstantiateWeapon();
        }

        weaponObject.Reposiotion(angle);
    } 


    public void DestroyWeapone()
    {
       
        weaponObject.gameObject.SetActive(false);
               
    }

    public override void AddItem()
    {
       
        base.AddItem();

    }


    public override void RemoveItem()
    {
        DestroyWeapone();
        base.RemoveItem();
    }

    public override void WaveEnd()
    {
        base.WaveEnd();
    }

    public override void UpdateItem()
    {
        if (weaponObject == null)
        {
            return;
        }

        weaponObject.MoveWeapon();
        
    }

    public override void EndWaveItemUpdateEvent()
    {
        weaponObject.EndWaveItemObject();
    }


}
