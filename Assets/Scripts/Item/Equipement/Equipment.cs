using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �߰�, ���� �Ҷ� �ϴܿ� slot�� �߰�, �����Ѵ�.
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
    /// �ۼ��� : ����; base.AddItem();
    /// </summary>
    virtual public void AddItem()
    {
        var itemUI = GameScene.Instance.GameRoopController.GetItemGameUI();

        var type =  InfoManager.Instance.TableItem.GetItemUseTypeById(Item_Id);

        instanceid = GameScene.Instance.ItemInstanceId;

        itemUI.ItemAdd(Item_Name, Count,instanceid, Item_Id, type);
    }


    /// <summary>
    /// �ۼ��� : ����; base.RemoveItem();
    /// </summary>
    virtual public void RemoveItem()
    {
        var itemUI = GameScene.Instance.GameRoopController.GetItemGameUI();
        itemUI.RemoveItem(Item_Id,instanceid);

        GameScene.Instance.GameRoopController.player.UnUseItem(Item_Id);
    }


    /// <summary>
    /// �ۼ��� : ����; base.WaveEnd();
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
