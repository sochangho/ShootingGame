using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//스테이지의 전체 게임 로직
public class GameRoopController : MonoBehaviourSingletonPersistent<GameRoopController>, IGameloop
{
    //-----------------------------------
    public Player player { get; private set; }
    
    public int CurrentStage { get; private set; }
    //-------------------------------------------


    [SerializeField]
    private CameraFollowPlayer cameraFollowPlayer;



    private GameEvents gameEvents = new GameEvents();

    public override void Awake()
    {
        base.Awake();
    }

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
        gameEvents.AddEndEvent(End);
    }


    private void Init()
    {
        Debug.Log("Init");
       player = GetPlayer();
       cameraFollowPlayer.SetFollowPlayer(player);
    }

    private void End()
    {
        player = null;
        cameraFollowPlayer = null;
    }

    private Player GetPlayer()
    {
        if (player == null)
        {
            string prefabPath = "Prefabs/Character/Players/Robot";
            var prefab = ResourceManager.Load<Player>(AssetType.Prefab, prefabPath);
            player = GameObject.Instantiate(prefab);
            player.transform.position = Vector3.zero;
        }

        return player;
    }


}
