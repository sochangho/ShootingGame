
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RandomManager 
{
    /// <summary>
    /// 누적확률 계산
    /// </summary>
    /// <param name="persentages"></param>
    public static TItem CumulativeProbability<TItem>(List<KeyValuePair<float , TItem>> persentages)
    {

        var sortedPersentage = persentages.OrderBy(x => x.Key).ToList();


        float max = 0;
        foreach(KeyValuePair<float , TItem> persentage in sortedPersentage)
        {
            max += persentage.Key;
        }

     
        float randomPoint = Random.value * max;   
        for(int i = 0; i < sortedPersentage.Count; i++)
        {
            if(randomPoint < sortedPersentage[i].Key)
            {
                return sortedPersentage[i].Value;
            }
            else
            {
                randomPoint -= sortedPersentage[i].Key;
            }
        }
        return sortedPersentage[sortedPersentage.Count - 1].Value;
    }





    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>

    public static T RandomDraw<T>(List<T> values)
    {

       int randomIndex  = Random.Range(0, values.Count);

        return values[randomIndex];
    }

}
