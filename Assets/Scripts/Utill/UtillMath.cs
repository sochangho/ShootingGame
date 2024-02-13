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



    /// <summary>
    /// 현재 타겟과의 관계에 따라 회전하기
    /// </summary>
    /// <param name="thisTransform">회전시킬 오브젝트</param>
    /// <param name="targetTransfrom"> 중심 위치</param>
    /// <param name="aixs"> 회전축 </param>
    /// <param name="speed"> 회전속도</param>
    static public void RotateAround(Transform thisTransform,in Vector3 targetPosition, in Vector3 aixs, float speed )
    {
        float t = speed * Time.deltaTime;
        thisTransform.RotateAround(targetPosition, aixs, t);
    }



    /// <summary>
    /// 타켓과 거리를 유지한 채로 회전
    /// </summary>
    /// <param name="thisTransform">회전시킬 오브젝트</param>
    /// <param name="targetPosition">중심 위치</param>
    /// <param name="axis">회전축 벡터</param>
    /// <param name="diff">(타겟의 위치 - 자신의 위치) 벡터</param>
    /// <param name="speed">회전 속도</param>
    /// <param name="t">현재 회전값을 기억할 변수</param>
    static public void RotateAroundDistance(Transform thisTransform, in Vector3 targetPosition ,in Vector3 axis, in Vector3 diff, 
        float speed, ref float t)
    {
        t += speed * Time.deltaTime;

        Vector3 offset = Quaternion.AngleAxis(t, axis) * diff;
        thisTransform.position = targetPosition + offset;
    }





    /// <summary>
    /// 타켓과 거리를 유지한 채로 회전
    /// </summary>
    /// <param name="thisTransform">회전시킬 오브젝트</param>
    /// <param name="targetPosition">중심 위치</param>
    /// <param name="axis">회전축 벡터</param>
    /// <param name="diff">(타겟의 위치 - 자신의 위치) 벡터</param>
    /// <param name="speed">회전 속도</param>
    /// <param name="t">현재 회전값을 기억할 변수</param>
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
        Debug.Log($"각도 벡터 {thisTransform.position}");

    }


}
