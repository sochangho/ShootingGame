using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtillMath
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="postion1">����Ʈ 1</param>
    /// <param name="position2">����Ʈ 2</param>
    /// <param name="closeDistance">�Ÿ�</param>
    /// <returns> �Ÿ����� ������ true �ƴϸ� flase </returns>

   static public bool TwoPointClosed(Vector3 postion1 , Vector3 position2, float closeDistance)
   {
        Vector3 offset = position2 - postion1;

        float sqrLen = offset.sqrMagnitude;

        return sqrLen < closeDistance * closeDistance;
   }



    /// <summary>
    /// ���� ������ = ���ݷ� * (1 - (���� / (100 + ����))
    /// </summary>
    /// <param name="attack">���ݷ�</param>
    /// <param name="defence">����</param>
    /// <returns> ���� ���ݷ� </returns>

    static public float DamageCalculate(float attack , float defence)
    {        
        float value = attack * (1 - (defence / (100 + defence)));

        return value;
    }




}
