using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactoryClass 
{
    
    public static void ItemAdd(Table_Item.IItemInfo itemInfo) 
    {
        if(itemInfo is Table_Item.ItemAbilityInfo)
        {
            Table_Item.ItemAbilityInfo itemAbilityInfo = itemInfo as Table_Item.ItemAbilityInfo;

            Ability item = new Ability(itemAbilityInfo);

            GameScene.Instance.GameRoopController.ItemAdd(item);

        }
        else if(itemInfo is Table_Item.ItemWeaponInfo)
        {
            Table_Item.ItemWeaponInfo itemWeaponInfo = itemInfo as Table_Item.ItemWeaponInfo;

            Weapon weapon = new Weapon(itemWeaponInfo);

            GameScene.Instance.GameRoopController.ItemAdd(weapon);

        }




    }

    public static string ItemSpritePath(Table_Item.IItemInfo itemInfo)
    {
        string path = string.Empty;

        if (itemInfo is Table_Item.ItemAbilityInfo)
        {
            path = $"{PathString.ITEM_SPRITE_ABILITY}/{itemInfo.GetName()}";

        }
        else if (itemInfo is Table_Item.ItemWeaponInfo)
        {
            path = $"{PathString.ITEM_SPRITE_WEAPON}/{itemInfo.GetName()}";
        }


        return path;
    }


    public static string ItemSpritePathByType(ItemUseType itemUseType, string nameSprite)
    {
        string path = string.Empty;

        if (itemUseType == ItemUseType.Ability)
        {
            path = $"{PathString.ITEM_SPRITE_ABILITY}/{nameSprite}";

        }
        else if (itemUseType == ItemUseType.Weapon)
        {
            path = $"{PathString.ITEM_SPRITE_WEAPON}/{nameSprite}";
        }


        return path;
    }

}
