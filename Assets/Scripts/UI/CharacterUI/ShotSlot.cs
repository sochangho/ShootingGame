using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotSlot : MonoBehaviour
{
    [SerializeField] private Image Image_shot;
    
    public void Use()
    {
        SetAlpha(0.5f);
       
    }

    public void UnUse()
    {
        SetAlpha(1f);
       

    }

    private void SetAlpha(float a)
    {
        Color color =  Image_shot.color;

        color.a = a;

        Image_shot.color = color;
    }

   
}
