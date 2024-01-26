using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtillMath
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="postion1">포인트 1</param>
    /// <param name="position2">포인트 2</param>
    /// <param name="closeDistance">거리</param>
    /// <returns> 거리보다 작으면 true 아니면 flase </returns>

   static public bool TwoPointClosed(Vector3 postion1 , Vector3 position2, float closeDistance)
   {
        Vector3 offset = position2 - postion1;

        float sqrLen = offset.sqrMagnitude;

        return sqrLen < closeDistance * closeDistance;
   }



    /// <summary>
    /// 최종 데미지 = 공격력 * (1 - (방어력 / (100 + 방어력))
    /// </summary>
    /// <param name="attack">공격력</param>
    /// <param name="defence">방어력</param>
    /// <returns> 최종 공격력 </returns>

    static public float DamageCalculate(float attack , float defence)
    {        
        float value = attack * (1 - (defence / (100 + defence)));

        return value;
    }




}
