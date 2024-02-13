using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameUI : MonoBehaviour
{
    private List<ItemUIElement> itemUIElements = new List<ItemUIElement>(); 

    public void ItemAdd(string name, int count, int instanceid, int id, ItemUseType itemUseType)
    {
        var e = ResourceManager.Load<ItemUIElement>(AssetType.Prefab, $"{PathString.PREFAB_ELEMENTS}/ItemElement");

        var clone = Instantiate(e, this.transform);

        clone.ItemAdd(name, count,instanceid ,id, itemUseType);

        itemUIElements.Add(clone);
    }

    public void RemoveItem(int id, int instanceid)
    {
        var e = itemUIElements.Find(x => x.ItemId == id && x.Instanceid == instanceid);

        if (e != null)
        {
            var r = e;
          
            itemUIElements.Remove(e);

            GameObject.Destroy(r.gameObject);
        }
    }


    public void WaveEnd(int id, int instanceid, int count)
    {
        var e = itemUIElements.Find(x => x.ItemId == id && x.Instanceid == instanceid);
         
        if(e != null)
        {
            if(count > 0)
            {
                e.WaveEnd(count);

            }
            else
            {
                itemUIElements.Remove(e);
                GameObject.Destroy(e);
            }
        }

    }




    
}
