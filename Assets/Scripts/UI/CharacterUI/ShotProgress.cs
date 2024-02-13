using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotProgress : MonoBehaviour
{
    private Stack<ShotSlot> unUseShot = new Stack<ShotSlot>();

    private Stack<ShotSlot> useShot = new Stack<ShotSlot>();


    [SerializeField]
    private Image image_ShotFill;

    private Coroutine coroutine;


    public void Charge(int count)
    {

        while(useShot.Count > 0)
        {
            var slot = useShot.Pop();
            
            slot.UnUse();

            unUseShot.Push(slot);
        }


        if(count > unUseShot.Count)
        {

            int totalCount = count - unUseShot.Count;

            for (int i = 0; i < totalCount ; ++i)
            {

            
               var slot =  ResourceManager.Load<ShotSlot>(AssetType.Prefab, $"{PathString.PROGRESS}/ShotSlot");

               var clone = Instantiate(slot , this.transform);

               clone.UnUse();

               unUseShot.Push(clone);
            }


        }
        else
        {
            int totalCount = unUseShot.Count - count;

            for (int i = 0; i < totalCount; ++i)
            {
                var slot = unUseShot.Pop();
                GameObject.Destroy(slot.gameObject);
            }
        }


    }


    public void Use()
    {

        if(unUseShot.Count > 0)
        {
            var slot = unUseShot.Pop();

            slot.Use();

            useShot.Push(slot);
        }

  
    }

    public void Recharge(PlayerInfo player, System.Action action = null)
    {

       StartCoroutine(ChargeingProgress(player, action));


    }

    public void SlotsSetActive(bool isActive)
    {
        for (int i = 1; i < this.transform.childCount; ++i)
        {
            this.transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }

    


    IEnumerator ChargeingProgress(PlayerInfo player, System.Action action )
    {
        image_ShotFill.fillAmount = 0;
        image_ShotFill.gameObject.SetActive(true);

        SlotsSetActive(false);

        while(image_ShotFill.fillAmount < 1)
        {
            image_ShotFill.fillAmount += Time.deltaTime * player.Shot_LoadSpeed;

            yield return null;
        }

        SlotsSetActive(true);

        action?.Invoke();

        Charge(player.Shot_Count);
               
        image_ShotFill.gameObject.SetActive(false);

    }





}
