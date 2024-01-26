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




