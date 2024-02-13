using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.Events;

public class UIManager : MonoBehaviourSingletonPersistent<UIManager>
{

    public GameObject dimmedPanel;

    private List<Popup> popupList = new List<Popup>();

    private Stack<Popup> popupStack = new Stack<Popup>();




    public void OpenPopup<T>(UnityAction action) where T : Popup
    {
        Type t = typeof(T);

        var popup =  popupList.Find(x => x.type.Equals(t));


        if (popup == null)
        {
            string path = $"{PathString.POPUP}/{typeof(T).Name}";

            var loadPopup = ResourceManager.Load<T>(AssetType.Prefab, path);

            popup = Instantiate(loadPopup, this.transform);

            popup.type = t;

          
            popupList.Add(popup);
        }


        popupStack.Push(popup);

        popup.Open(action);

        Dimmed();


        
    }





    public void ClosePopup<T>(UnityAction action) where T : Popup
    {
        Type t = typeof(T);

        while (popupStack.Count > 0)
        {
           var p =  popupStack.Pop();

           p.Close(action);


           if (p.type.Equals(t))
           {
                break;
           }
        }

        Dimmed();
    }






    public void Dimmed()
    {

        if (popupStack.Count > 0)
        {

            dimmedPanel.SetActive(true);

            dimmedPanel.transform.SetSiblingIndex(transform.childCount - 2);
        }
        else
        {
            dimmedPanel.SetActive(false);
        }
    }


    
}
