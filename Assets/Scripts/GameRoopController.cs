using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스테이지의 전체 게임 루프
public class GameRoopController : MonoBehaviour, IGameloop
{
    //-----------------------------------
    public Player player { get; private set; }
    
    public int CurrentStage { get; private set; }
    //-------------------------------------------


    [SerializeField]
    private CameraFollowPlayer cameraFollowPlayer;

    [SerializeField]
    private List<Transform> spwanPoints;

    private List<KeyValuePair<float, int>> enemySpwanData; // 소환될 몬스터 확률 // 가중치 , 적 id

    private GameEvents gameEvents = new GameEvents();

    private Coroutine enemyCreateRoutine;

    private bool isGameEnd = false;

    public void GameStart()
    {
        gameEvents.GameStart();
    }

    public void GameEnd()
    {
        gameEvents.GameEnd();
    }

    public void AddEvents()
    {
        gameEvents.AddStartEvent(Init);
        gameEvents.AddStartEvent(StartEnemyCreate);

        gameEvents.AddEndEvent(End);
        gameEvents.AddEndEvent(EndEnemyCreate);
    }

    public Transform GetCameraTransform()
    {
        return cameraFollowPlayer.transform;
    }


    private void Init()
    {
        Debug.Log("Init");

        
        //플레이어 생성 
        player = GetPlayer(); 
        
        cameraFollowPlayer.SetFollowPlayer(player);
        //

    }



    private void End()
    {
        
        player = null;
        cameraFollowPlayer = null;
    }

    private void EnemyCreat()
    {


        if (enemySpwanData == null)
        {
            enemySpwanData = new List<KeyValuePair<float, int>>();
        }

        for(int i = enemySpwanData.Count - 1; i >= 0; --i)
        {
            enemySpwanData.RemoveAt(i);
        }


        var currEnemyList = InfoManager.Instance.TableStage.GetStageInfos(1, (int)GameObjectType.Enemy);

        for (int i = 0; i < currEnemyList.Count; ++i)
        {
            enemySpwanData.Add(new KeyValuePair<float, int>(currEnemyList[i].Weight, currEnemyList[i].Id));
        }


    }



    private void StartEnemyCreate()
    {
        isGameEnd = false;

        EnemyCreat();
        enemyCreateRoutine = StartCoroutine(EnemyCreateRoutine());
    }

    private void EndEnemyCreate()
    {
        isGameEnd = true;
        
        StopCoroutine(enemyCreateRoutine);
    }

    
    private Player GetPlayer()
    {
        if (player == null)
        {
            int id = InfoManager.Instance.GetCurrentPlayerCharacterID();
            var info = InfoManager.Instance.TablePlayer.GetInfoById(id);
            
            string prefabPath = $"Prefabs/Character/Players/{info.CharacterName}";
            var prefab = ResourceManager.Load<Player>(AssetType.Prefab, prefabPath);
            
            player = GameObject.Instantiate(prefab);
            player.CharacterCreated(id); 
            player.transform.position = Vector3.zero;

        }

        return player;
    }

    private Enemy GetEnemy(int enemyId , Vector3 spwanPosition)
    { 
        var enemy = GameScene.Instance.ObjectPoolManager.UseObject(GameObjectType.Enemy, enemyId, spwanPosition);


        Enemy e = null;

        if (enemy != null && enemy.GetComponent<Enemy>() != null )
        {
            e = enemy.GetComponent<Enemy>();
            e.CharacterCreated(enemyId);
        }

        return e;
    }


    IEnumerator EnemyCreateRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(5f);

        while (!isGameEnd)
        {
            var point =  RandomManager.RandomDraw<Transform>(spwanPoints);

            Vector3 spwanPosition = point.position; // 생성 위치 

            int enemyid = RandomManager.CumulativeProbability<int>(enemySpwanData);

            GetEnemy(enemyid, spwanPosition);

            yield return waitForSeconds;
        }

    }
    

}
