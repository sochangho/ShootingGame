
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpProgress : MonoBehaviour , IObserver
{

    [SerializeField]
    private Slider slider_hp;

    [SerializeField]
    private TextMeshProUGUI text_hp; 

    public void UpdateData(object data)
    {
        float origine = (float)data;

        slider_hp.value = origine;

        text_hp.text = origine.ToString();
       
    }

 


}
