using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using UnityEngine.EventSystems;

public class RewardSlot : Slot<RewardPopup>
{
    [SerializeField] private TextMeshProUGUI tmp_ItemName;

    [SerializeField] private TextMeshProUGUI tmp_ItemSubscript;

    [SerializeField] private Image image_Item;
        
    [SerializeField] private GameObject selectObj;

    Table_Item.IItemInfo itemInfo;

    public override void Created()
    {
        base.Created();

        SettingUI();
    }

    public void SettingUI()
    {
        itemInfo = InfoManager.Instance.TableItem.GetRandomItemInfo();

        var path =  ItemFactoryClass.ItemSpritePath(itemInfo);

        image_Item.sprite = ResourceManager.SpriteLoad(path);

        tmp_ItemName.text = itemInfo.GetName();

        tmp_ItemSubscript.text = itemInfo.GetSubscript();

    }

    public void SlotSelected()
    {

        if (IsSelect)
        {

            ItemFactoryClass.ItemAdd(itemInfo);
        }

    }



    public override void Select()
    {
      
        parentPopup.AllCheckRelese();

        base.Select();

        selectObj.SetActive(true);

    }

    public override void UnSelect()
    {
        base.UnSelect();

        selectObj.SetActive(false);
    }


}
