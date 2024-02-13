using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 추가, 삭제 할때 하단에 slot을 추가, 삭제한다.
/// </summary>

public abstract class Equipment : Item
{
    public int Count { get; protected set; }

    public override void UseItem()
    {
        AddItem();   
    }


    public override void UnUseItem()
    {
        RemoveItem();
    }


    /// <summary>
    /// 작성법 : 로직; base.AddItem();
    /// </summary>
    virtual public void AddItem()
    {
        var itemUI = GameScene.Instance.GameRoopController.GetItemGameUI();

        var type =  InfoManager.Instance.TableItem.GetItemUseTypeById(Item_Id);

        instanceid = GameScene.Instance.ItemInstanceId;

        itemUI.ItemAdd(Item_Name, Count,instanceid, Item_Id, type);
    }


    /// <summary>
    /// 작성법 : 로직; base.RemoveItem();
    /// </summary>
    virtual public void RemoveItem()
    {
        var itemUI = GameScene.Instance.GameRoopController.GetItemGameUI();
        itemUI.RemoveItem(Item_Id,instanceid);

        GameScene.Instance.GameRoopController.player.UnUseItem(Item_Id);
    }


    /// <summary>
    /// 작성법 : 로직; base.WaveEnd();
    /// </summary>
    virtual public void WaveEnd()
    {
        Count--;

        if(Count <= 0)
        {
            RemoveItem();
          
        }
                
        var itemUI = GameScene.Instance.GameRoopController.GetItemGameUI();
        itemUI.WaveEnd(Item_Id,instanceid,Count);
      
    }

    abstract public void UpdateItem();

    abstract public void EndWaveItemUpdateEvent();

}
