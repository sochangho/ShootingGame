using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// �� ���� ��ġ �� ������ ���� ���� 
/// </summary>
public class GameScene : MonoBehaviour , IGameloop
{

    

    void Awake()
    {
        GameStart();

    }


    public void GameStart()
    {
        AddEvents();
        ObjectPoolManager.Instance.GameStart();
        GameRoopController.Instance.GameStart(); 
    }

    public void GameEnd()
    {
        ObjectPoolManager.Instance.GameEnd();
        GameRoopController.Instance.GameEnd();
    }

    public void AddEvents()
    {
        ObjectPoolManager.Instance.AddEvents();
        GameRoopController.Instance.AddEvents();
    }




}




