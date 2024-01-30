using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스테이지의 전체 게임 루프
public class GameRoopController : MonoBehaviour, IGameloop ,IObserver
{
    //-----------------------------------
    public Player player { get; private set; }

    public int CurrentStageIndex { get; private set; } = 0;
    //-------------------------------------------


    [SerializeField]
    private CameraFollowPlayer cameraFollowPlayer;

    [SerializeField]
    private List<Transform> spwanPoints;

    [SerializeField]
    private GameProgress gameProgress;

    [SerializeField]
    private MoveVirtualJoystick moveVirtualJoystick;

    [SerializeField]
    private AttackVirtualJoystick attackVirtualJoystick;


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
        gameEvents.AddStartEvent(StartStage);        
        //gameEvents.AddStartEvent(StartEnemyCreate); 스테이지 루프 안에서 호출 될 것임

        gameEvents.AddEndEvent(End);
        gameEvents.AddEndEvent(EndEnemyCreate);
        gameEvents.AddEndEvent(gameProgress.GameEnd);
    }

    public void UpdateData(object data)
    {
        string d = (string)data;

        if (d.Equals("Player"))
        {
            //게임 결과
            ResultGame();
        }
        else
        {
            EndStage();

        }
    }

    

    #region 스테이지 루프


    public void StartStage()
    {
        //플레이어 리스폰 위치 , 조이스틱 비활성화 및 못움직이게하기 
        player.SetJoistick(moveVirtualJoystick, attackVirtualJoystick);

        moveVirtualJoystick.gameObject.SetActive(false);

        attackVirtualJoystick.gameObject.SetActive(false);

        player.DontMovePlayer();

        player.transform.position = Vector3.zero;


        //카운트 시작
        //카운트 종료  ,조이스틱 활성화 및 움직이게하기 

        StartCoroutine(StartCountRoutine
            (()=> { 
            // 게임 시작 ui 메서드 호출
            //{구현}
            
            },
            ()=> { 
            // 게임 카운트 ui 메서드 호출 
            //{구현}
            
            }, 
            () => {

                
                moveVirtualJoystick.gameObject.SetActive(true);

                attackVirtualJoystick.gameObject.SetActive(true);

                player.IsMovePlayer();

                StartEnemyCreate();

                int curStage =  GetCurrentStage();

                var info = InfoManager.Instance.TableStage.GetStageUniqeInfo(curStage);

                gameProgress.GameStart(info.Time);
               
            }));


    }




    /// <summary>
    /// 플레이어가 죽을 때, 시간이 종료 되었을 때 호출 됨
    /// 플에이러가 죽을 때 호출 인지, 시간이 종료 되어서 호출 된 건지 확인
    /// 외부에서 호출
    /// (시간 체크)Observer이다 
    /// data 가 0 일 때 호출 
    /// </summary>
    public void EndStage()
    {
        //시간 체크 종료 
        gameProgress.GameEnd();

        //몬스터 삭제
        EndEnemyCreate();
        StartCoroutine(Delay(1f, RewardStage));
        
    }



    /// <summary>
    /// 게임 결과창
    /// 버튼 클릭시 게임종료 
    /// </summary>
    public void ResultGame()
    {
        gameProgress.GameEnd();
    }



    /// <summary>
    /// ui 창 출력 아이템 선택 후 
    /// 버튼 누를 경우  Next Stage 호출
    /// </summary>
    public void RewardStage()
    {

        //플레이어 리스폰 위치 , 조이스틱 비활성화 및 못움직이게하기 

        player.SetJoistick(moveVirtualJoystick, attackVirtualJoystick);

        moveVirtualJoystick.gameObject.SetActive(false);

        attackVirtualJoystick.gameObject.SetActive(false);

        player.DontMovePlayer();


        //보상창 출력

        //선택 



    }



    public void NextStage()
    {

        // 스테이지 비교 마지막 스테이지 인지??
        // 다음 스테이지 or 게임종료

        CurrentStageIndex++;
  
        if(GetCurrentStage() < 0)
        {
            ResultGame();
        }
        else
        {
            StartStage();
        }      
    }

#endregion



    public Transform GetCameraTransform()
    {
        return cameraFollowPlayer.transform;
    }

    public int GetCurrentStage()
    {
        return InfoManager.Instance.TableStage.CurrentStage(CurrentStageIndex);
    }


    private void Init()
    {
        Debug.Log("Init");

        
        //플레이어 생성 
        player = GetPlayer(); 
        
        cameraFollowPlayer.SetFollowPlayer(player);

        gameProgress.ResisterObserver(this);

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
        GameScene.Instance.ObjectPoolManager.Delete();
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

            player.ResisterObserver(this);

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
    
    IEnumerator StartCountRoutine(System.Action startaAtion = null,System.Action progressAction = null, System.Action finishAction = null)   
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

        Debug.Log($"<color=magenta> 카운트 시작 </color>");

        startaAtion?.Invoke();

        for (int i = 0; i < 3; ++i)
        {
            yield return waitForSeconds;

            Debug.Log($"<color=magenta> 카운트 : {i+1} </color>");

            progressAction?.Invoke();
        }

        finishAction?.Invoke();
        Debug.Log($"<color=magenta> 카운트 종료 </color>");

    }


    /// <summary>
    /// 몇초 뒤에 호출
    /// </summary>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    IEnumerator Delay(float time, System.Action action)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(time);

        yield return waitForSeconds;

        action?.Invoke();


    }


}
