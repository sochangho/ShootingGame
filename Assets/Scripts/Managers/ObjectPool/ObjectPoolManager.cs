using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameObjectType
{ 
    Player = 0,     // 플레이어

    Projectile = 1, //발사체

    Enemy = 2, //적

    Reward = 3,

    Size
}


public class ObjectPoolManager : MonoBehaviourSingletonPersistent<ObjectPoolManager>
{
    
    public Dictionary<int, ObjectPool>[] Dic_ObjectPool { get; private set;} // Key Pid , Value ObjectPool

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
        gameEvents.AllDelete();
    }

    public void AddEvents()
    {
        gameEvents.AddStartEvent(Initialized);
        gameEvents.AddEndEvent(Delete);
    }



    #region Init
    private void Initialized()
    {
        Dic_ObjectPool = new Dictionary<int, ObjectPool>[(int)GameObjectType.Size];

        for(int i = 0; i < (int)GameObjectType.Size; ++i)
        {
            Dic_ObjectPool[i] = new Dictionary<int, ObjectPool>();
        }


    }

    #endregion

    #region End
    private void Delete()
    {
        for(int i = 0; i < (int)GameObjectType.Size; ++i)
        {
            foreach(var d in Dic_ObjectPool[i])
            {
                var op = d.Value;
                op.AllDelete();
            }

            Dic_ObjectPool[i] = null;
        }

    }

    #endregion


    #region ObjectPool



    public GameObject UseObject(GameObjectType gameObjectType, int id, Vector3 postion)
    {
        var dic =  Dic_ObjectPool[(int)gameObjectType];

        if (!dic.ContainsKey(id))
        {
            int capacity = GetCapacityByIdAndGameObjectType(gameObjectType, id, GameRoopController.Instance.CurrentStage);
            dic.Add(id, ObjectPoolFactory(gameObjectType, id, capacity));

        }

        return dic[id].UseObject(postion);
    } 




    public void UnUseObject(ObjectPooling objectPooling)
    {
        GameObjectType gameObjectType = objectPooling.Type;
        int id = objectPooling.Id;


        var dic = Dic_ObjectPool[(int)gameObjectType];

        if (!dic.ContainsKey(id))
        {

            int capacity = GetCapacityByIdAndGameObjectType(gameObjectType, id, GameRoopController.Instance.CurrentStage);

            dic.Add(id, ObjectPoolFactory(gameObjectType , id ,capacity));

        }

        dic[id].UnUseObject(objectPooling.gameObject);

    }


    
    public ObjectPool ObjectPoolFactory(GameObjectType gameObjectType, int id, int capacity)
    {
        ObjectPool objectPool = new ObjectPool();

        objectPool.Set(gameObjectType, id, capacity);

        return objectPool;
    }

    public int GetCapacityByIdAndGameObjectType(GameObjectType gameObjectType , int id, int currrentStage)
    {
       var stageInfo =  InfoManager.Instance.TableStage;
       var infos = stageInfo.GetStageInfos(currrentStage, (int)gameObjectType);
       
        if (infos == null)
            return 0;

        var info = infos.Find(x => x.Id == id);

        if (info == null)
            return 0;


        return info.Capacity;
    }



    #endregion

}



public class ObjectPool
{
    #region public

    public GameObjectType GameObjectType { get; private set; }

    public int Id { get; private set; }   

    public int Capacity { get; private set; }

    #endregion

    #region private

    private string path; 


    private Stack<GameObject> stack_GameObject = new Stack<GameObject>();

    #endregion

    public void Set(GameObjectType gameObjectType, int id, int capacity)
    {
        GameObjectType = gameObjectType;
        
        Id = id;

        Capacity = capacity;
        
        switch (gameObjectType)
        {
            case GameObjectType.Enemy:
                {
                    var data =  InfoManager.Instance.TableEnemys.GetData(id);

                    path = data.Path;

                    break;
                }

        }


    }

    // Pop
    public GameObject UseObject(Vector3 position)
    {
        GameObject clone = null;

        if(stack_GameObject.Count < Capacity)
        {
           var go =  ResourceManager.Load<GameObject>(AssetType.Prefab , path);
           clone = GameObject.Instantiate(go);
           clone.AddComponent<ObjectPooling>().Set(Id, GameObjectType);           
        }
        else
        {
           clone =  stack_GameObject.Pop();
        }

        clone.SetActive(true);
        clone.transform.localPosition = position;
        return clone;
    }


    // push
    public void UnUseObject(GameObject gameObject)
    {

        if(stack_GameObject.Count >= Capacity)
        {
            GameObject.Destroy(gameObject);
        }

        gameObject.SetActive(false);

        stack_GameObject.Push(gameObject);

    } 

    public void AllDelete()
    {

        while(stack_GameObject.Count > 0)
        {
            var go = stack_GameObject.Pop();
            GameObject.Destroy(go);
        }


    }


}