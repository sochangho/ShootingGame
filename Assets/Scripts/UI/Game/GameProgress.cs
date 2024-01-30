using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgress : MonoBehaviour, ISubject
{
    [SerializeField]
    private Image imageFill;

    private List<IObserver> observers = new List<IObserver>();

    private Coroutine timeCoroutine;

   public void ResisterObserver(IObserver observer)
   {
        observers.Add(observer);
   }

   public void RemoveObserver(IObserver observer)
   {
        observers.Remove(observer);
   }

   public void NotifyObserver()
   {
        foreach(var o in observers)
        {
            o.UpdateData(typeof(GameProgress).Name);
        }

   }


    public void GameStart(int time)
    {

       timeCoroutine = StartCoroutine(GameProgressTimeRoutine(time));

    }
   
    public void GameEnd()
    {
        if (timeCoroutine != null)
        {
            StopCoroutine(timeCoroutine);
        }
    }

  
   IEnumerator GameProgressTimeRoutine(int timeTotal )
   {
        float curTime = 0;


        while(timeTotal > curTime)
        {

            curTime += Time.deltaTime;

           float value = 1f - curTime / (float)timeTotal;

            imageFill.fillAmount = value;

            yield return null;
        }

        NotifyObserver();
   }


}
