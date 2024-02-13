using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot<T> : BaseObject where T : Popup
{
    [SerializeField] protected Button button_Slot;

    protected T parentPopup;

    public bool IsSelect { get; private set; }

    override public void Created()
    {
        if (button_Slot == null)
        {
            return;
        }

        button_Slot.onClick.AddListener(Select);
    }
    override public void Destroyed() { }
    override public void Active() { }
    override public void InActive() { }


    virtual public void SetPopup(T popup)
    {
        parentPopup = popup;

    }
    
    virtual public void Select()
    {
        IsSelect = true;
    }

    virtual public void UnSelect()
    {
        IsSelect = false;
    }


}
