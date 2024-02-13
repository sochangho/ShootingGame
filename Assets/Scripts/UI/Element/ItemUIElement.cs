using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ItemUIElement : MonoBehaviour
{
    [SerializeField] private Image image_Item;
    
    [SerializeField] private TextMeshProUGUI tmp_Count;

    private ItemGameUI itemGameUI;

    public int Instanceid { get; private set; } 

    public int ItemId { get; private set; }
    public string ItemName { get; private set; }
    public int Count { get; private set; }

    public void ItemAdd(string name,int count,int instanceid, int id, ItemUseType itemUseType)
    {

        this.Instanceid = instanceid;
        ItemName = name;
        ItemId = id;
        Count = count;


        var Item = ResourceManager.Load<Sprite>(AssetType.Sprite,ItemFactoryClass.ItemSpritePathByType(itemUseType,ItemName));

        image_Item.sprite = Item;

        tmp_Count.text = Count.ToString();
    }

    public void ItemRemove()
    {
        //»èÁ¦
    }



    public void WaveEnd(int count)
    {
        Count = count;

        if(Count > 0)
        {

            tmp_Count.text = Count.ToString();

        }
   

    }


}
