using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class ResultPopup : Popup
{
    [SerializeField]
    private Button button;

    public override void Created()
    {
        base.Created();

        button.onClick.AddListener(

            () =>
            {
                SceneManager.LoadScene("StartScene");
            }
            
            );
    }


    public override void Open(UnityAction action = null)
    {
        base.Open(action);
    }

    public override void Close(UnityAction action = null)
    {
        base.Close(action);
    }


}
