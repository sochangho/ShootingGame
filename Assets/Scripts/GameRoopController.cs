using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���������� ��ü ���� ����
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


    private List<KeyValuePair<float, int>> enemySpwanData; // ��ȯ�� ���� Ȯ�� // ����ġ , �� id

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
        //gameEvents.AddStartEvent(StartEnemyCreate); �������� ���� �ȿ��� ȣ�� �� ����

        gameEvents.AddEndEvent(End);
        gameEvents.AddEndEvent(EndEnemyCreate);
        gameEvents.AddEndEvent(gameProgress.GameEnd);
    }

    public void UpdateData(object data)
    {
        string d = (string)data;

        if (d.Equals("Player"))
        {
            //���� ���
            ResultGame();
        }
        else
        {
            EndStage();

        }
    }

    

    #region �������� ����


    public void StartStage()
    {
        //�÷��̾� ������ ��ġ , ���̽�ƽ ��Ȱ��ȭ �� �������̰��ϱ� 
        player.SetJoistick(moveVirtualJoystick, attackVirtualJoystick);

        moveVirtualJoystick.gameObject.SetActive(false);

        attackVirtualJoystick.gameObject.SetActive(false);

        player.DontMovePlayer();

        player.transform.position = Vector3.zero;


        //ī��Ʈ ����
        //ī��Ʈ ����  ,���̽�ƽ Ȱ��ȭ �� �����̰��ϱ� 

        StartCoroutine(StartCountRoutine
            (()=> { 
            // ���� ���� ui �޼��� ȣ��
            //{����}
            
            },
            ()=> { 
            // ���� ī��Ʈ ui �޼��� ȣ�� 
            //{����}
            
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
    /// �÷��̾ ���� ��, �ð��� ���� �Ǿ��� �� ȣ�� ��
    /// �ÿ��̷��� ���� �� ȣ�� ����, �ð��� ���� �Ǿ ȣ�� �� ���� Ȯ��
    /// �ܺο��� ȣ��
    /// (�ð� üũ)Observer�̴� 
    /// data �� 0 �� �� ȣ�� 
    /// </summary>
    public void EndStage()
    {
        //�ð� üũ ���� 
        gameProgress.GameEnd();

        //���� ����
        EndEnemyCreate();
        StartCoroutine(Delay(1f, RewardStage));
        
    }



    /// <summary>
    /// ���� ���â
    /// ��ư Ŭ���� �������� 
    /// </summary>
    public void ResultGame()
    {
        gameProgress.GameEnd();
    }



    /// <summary>
    /// ui â ��� ������ ���� �� 
    /// ��ư ���� ���  Next Stage ȣ��
    /// </summary>
    public void RewardStage()
    {

        //�÷��̾� ������ ��ġ , ���̽�ƽ ��Ȱ��ȭ �� �������̰��ϱ� 

        player.SetJoistick(moveVirtualJoystick, attackVirtualJoystick);

        moveVirtualJoystick.gameObject.SetActive(false);

        attackVirtualJoystick.gameObject.SetActive(false);

        player.DontMovePlayer();


        //����â ���

        //���� 



    }



    public void NextStage()
    {

        // �������� �� ������ �������� ����??
        // ���� �������� or ��������

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

        
        //�÷��̾� ���� 
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

            Vector3 spwanPosition = point.position; // ���� ��ġ 

            int enemyid = RandomManager.CumulativeProbability<int>(enemySpwanData);

            GetEnemy(enemyid, spwanPosition);

            yield return waitForSeconds;
        }

    }
    
    IEnumerator StartCountRoutine(System.Action startaAtion = null,System.Action progressAction = null, System.Action finishAction = null)   
    {

        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);

        Debug.Log($"<color=magenta> ī��Ʈ ���� </color>");

        startaAtion?.Invoke();

        for (int i = 0; i < 3; ++i)
        {
            yield return waitForSeconds;

            Debug.Log($"<color=magenta> ī��Ʈ : {i+1} </color>");

            progressAction?.Invoke();
        }

        finishAction?.Invoke();
        Debug.Log($"<color=magenta> ī��Ʈ ���� </color>");

    }


    /// <summary>
    /// ���� �ڿ� ȣ��
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
