using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RewardPopup : Popup
{
    private List<RewardSlot> slots;

    [SerializeField] private Button button_NextStage;

    [SerializeField] private Transform slotParent;

    public override void Created()
    {
        base.Created();

    }

    public override void InActive()
    {
        base.InActive();
        DestorySlot();
    }




    public void AllCheckRelese()
    {
        for(int i =0; i < slots.Count; ++i)
        {
            slots[i].UnSelect();            
        }
        
    }

    public override void Open(UnityAction action = null)
    {
        base.Open(action);
      
        if(action == null)
        {
            return;
        }

        InstantiateSlot();


        button_NextStage.onClick.RemoveAllListeners();

        button_NextStage.onClick.AddListener(SelectedItemAdd);
        button_NextStage.onClick.AddListener(action);
    }

    public void SelectedItemAdd()
    {
        for(int i = 0; i < slots.Count; ++i)
        {
            slots[i].SlotSelected();
        }


    }

    private void InstantiateSlot()
    {
        string path = $"{PathString.POPUP}/RewardSlot";

        if(slots == null)
        {
            slots = new List<RewardSlot>();
        }


        for (int i = 0; i < 3; ++i)
        {

            var slot = ResourceManager.Load<RewardSlot>(AssetType.Prefab, path);

            var clone = Instantiate(slot, slotParent);

            clone.SetPopup(this);
            clone.UnSelect();
            slots.Add(clone);
        }

    }

     
    private void DestorySlot()
    {
        for(int i = slots.Count - 1; i >= 0; --i)
        {
            GameObject.Destroy(slots[i].gameObject);
            slots.RemoveAt(i);
        }



    }

  
}
