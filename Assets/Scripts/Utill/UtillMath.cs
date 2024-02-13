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



    /// <summary>
    /// ���� Ÿ�ٰ��� ���迡 ���� ȸ���ϱ�
    /// </summary>
    /// <param name="thisTransform">ȸ����ų ������Ʈ</param>
    /// <param name="targetTransfrom"> �߽� ��ġ</param>
    /// <param name="aixs"> ȸ���� </param>
    /// <param name="speed"> ȸ���ӵ�</param>
    static public void RotateAround(Transform thisTransform,in Vector3 targetPosition, in Vector3 aixs, float speed )
    {
        float t = speed * Time.deltaTime;
        thisTransform.RotateAround(targetPosition, aixs, t);
    }



    /// <summary>
    /// Ÿ�ϰ� �Ÿ��� ������ ä�� ȸ��
    /// </summary>
    /// <param name="thisTransform">ȸ����ų ������Ʈ</param>
    /// <param name="targetPosition">�߽� ��ġ</param>
    /// <param name="axis">ȸ���� ����</param>
    /// <param name="diff">(Ÿ���� ��ġ - �ڽ��� ��ġ) ����</param>
    /// <param name="speed">ȸ�� �ӵ�</param>
    /// <param name="t">���� ȸ������ ����� ����</param>
    static public void RotateAroundDistance(Transform thisTransform, in Vector3 targetPosition ,in Vector3 axis, in Vector3 diff, 
        float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, axis) * diff;
        thisTransform.position = targetPosition + offset;
    }





    /// <summary>
    /// Ÿ�ϰ� �Ÿ��� ������ ä�� ȸ��
    /// </summary>
    /// <param name="thisTransform">ȸ����ų ������Ʈ</param>
    /// <param name="targetPosition">�߽� ��ġ</param>
    /// <param name="axis">ȸ���� ����</param>
    /// <param name="diff">(Ÿ���� ��ġ - �ڽ��� ��ġ) ����</param>
    /// <param name="speed">ȸ�� �ӵ�</param>
    /// <param name="t">���� ȸ������ ����� ����</param>
    static public void RotateAroundDistanceLookAt(Transform thisTransform, in Vector3 targetPosition, in Vector3 axis, in Vector3 diff,
        float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, Vector3.up) * diff;
        thisTransform.position = targetPosition + offset;

        Quaternion rot = Quaternion.LookRotation(-offset, axis);
        thisTransform.rotation = rot;
    }

    static public void RotateAroundDistanceInit(Transform thisTransform, in Vector3 targetPosition, in Vector3 axis, in Vector3 diff, float angle)
    {

      

        Vector3 offset = Quaternion.AngleAxis(angle, Vector3.up) * diff;
        thisTransform.position = targetPosition + offset;

        Debug.Log($"Offet {offset}");
        Debug.Log($"���� ���� {thisTransform.position}");

    }


}
