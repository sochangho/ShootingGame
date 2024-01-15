using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// �� ���� ��ġ �� ������ ���� ���� 
/// </summary>
public class GameScene : MonoBehaviour
{

    public List<MoveTwoPoint> points;

    public List<Enemy> enemies; 

    public float Interval; // ���� ����

    public bool isRun = false;
   
    public void Awake()
    {
        CalcuatePoints();
    }


    public void CalcuatePoints()
    {
        for(int i = 0; i < points.Count; ++i)
        {
            points[i].CalculateDirection();
        }
    }

    


    IEnumerator CreateRoutin()
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(Interval);
        isRun = true;
        while (isRun)
        {
           var enemy = RandomManager.RandomDraw<Enemy>(enemies);


            yield return waitForSeconds;
        }


    }




}



[System.Serializable]
public class MoveTwoPoint
{
    [SerializeField]
    private Transform startPoint;

    [SerializeField]
    private Transform endPoint;

    public Vector3 Direction { get; private set; }


    public void CalculateDirection()
    {
        Vector3 dir =  endPoint.localPosition - startPoint.localPosition;
        Direction = dir.normalized;

    }

    
}
