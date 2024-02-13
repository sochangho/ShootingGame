using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp_Count;

    public void CountShow(string text)
    {
        tmp_Count.text = text;
    }



}
