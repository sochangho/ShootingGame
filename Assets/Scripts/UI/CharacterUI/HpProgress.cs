using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpProgress : MonoBehaviour , IObserver
{
    [SerializeField]
    public Image imageFill;
    
    public void UpdateData(object data)
    {
        float origine =  imageFill.fillAmount;

        imageFill.fillAmount = (float)data;
    }

 


}
