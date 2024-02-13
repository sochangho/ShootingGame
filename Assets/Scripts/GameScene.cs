using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// 利 积己 困摹 棺 酒捞袍 积己 包府 
/// </summary>
public class GameScene : MonoBehaviourSingletonPersistent<GameScene> , IGameloop
{

    public ObjectPoolManager ObjectPoolManager;
    public GameRoopController GameRoopController;

    private int item_instanceId = 0;

    public int ItemInstanceId
    {
        get
        {
            item_instanceId++;
            return item_instanceId;
        }
    }


    public override void Awake()
    {
        base.Awake();
        GameStart();
    }


    public void GameStart()
    {
        AddEvents();

        StartCoroutine(DelayRoutin(() =>
        {
            InfoManager.Instance.LoadDatas();

            ObjectPoolManager.GameStart();
            GameRoopController.GameStart();

        }));

    }

    public void GameEnd()
    {
        ObjectPoolManager.GameEnd();
        GameRoopController.GameEnd();
    }

    public void AddEvents()
    {
        ObjectPoolManager.AddEvents();
        GameRoopController.AddEvents();
    }


    IEnumerator DelayRoutin(System.Action action)
    {
        yield return new WaitUntil(() => { return InfoManager.Instance != null; });

        action?.Invoke();
       
    }

}




