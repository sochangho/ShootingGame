using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameObjectType
{ 
    Player = 0,     // 플레이어

    Projectile = 1, //발사체

    Enemy = 2, //적

    Reward = 3,

    Effect = 4,

    Size
}


public class ObjectPoolManager : MonoBehaviour , IGameloop
{
    
    public Dictionary<int, ObjectPool>[] Dic_ObjectPool { get; private set;} // Key Pid , Value ObjectPool

    private GameEvents gameEvents = new GameEvents();



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
    public void Delete()
    {
        for(int i = 0; i < (int)GameObjectType.Size; ++i)
        {
            foreach(var d in Dic_ObjectPool[i])
            {
                var op = d.Value;
                op.AllDelete();
            }

            //Dic_ObjectPool[i] = null;
        }

    }

    #endregion


    #region ObjectPool



    public GameObject UseObject(GameObjectType gameObjectType, int id, Vector3 postion )
    {
        var dic =  Dic_ObjectPool[(int)gameObjectType];

        if (!dic.ContainsKey(id))
        {
            
            dic.Add(id, ObjectPoolFactory(gameObjectType, id));

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

            return;

        }

        dic[id].UnUseObject(objectPooling.gameObject);

    }


    
    public ObjectPool ObjectPoolFactory(GameObjectType gameObjectType, int id)
    {
        ObjectPool objectPool = new ObjectPool();

        objectPool.Set(gameObjectType, id);

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


    private Stack<GameObject> stack_GameObject = new Stack<GameObject>(); //

    private List<GameObject> using_GameObject = new List<GameObject>();

    #endregion

    public void Set(GameObjectType gameObjectType, int id)
    {
        GameObjectType = gameObjectType;
        
        Id = id;
                
        switch (gameObjectType)
        {
            case GameObjectType.Enemy:
            {
               var data =  InfoManager.Instance.TableEnemys.GetData(id);
               path = $"{PathString.PREFAB_ENEMY}/{data.Name}";
               Capacity = data.Capacity;
               break;
            }
            case GameObjectType.Projectile:
            {
               var data = InfoManager.Instance.TableShot.GetInfoById(id);
               path = $"{PathString.PREFAB_PROJECTILE}/{data.Shot_Name}";
               Capacity = data.Shot_Capacity;

               break;
            }
            case GameObjectType.Effect:
            {
                    var data = InfoManager.Instance.TableEffect.GetInfoById(id);
                    path = $"{PathString.PREFAB_EFFECT}/{data.Name}";
                    Capacity = data.Capacity;
                    break;
            }

        }


    }

    // Pop
    public GameObject UseObject(Vector3 position)
    {
        GameObject clone = null;

        if(stack_GameObject.Count + using_GameObject.Count  < Capacity)
        {
           var go =  ResourceManager.Load<GameObject>(AssetType.Prefab , path);
           clone = GameObject.Instantiate(go);
           clone.AddComponent<ObjectPooling>().Set(Id, GameObjectType);           
        }
        else
        {

            if (stack_GameObject.Count > 0)
            {
                clone = stack_GameObject.Pop();
            }
            else
            {
                return null;
            }
        }

        clone.SetActive(true);
        clone.transform.localPosition = position;

        using_GameObject.Add(clone);
        
        return clone;
    }


    // push
    public void UnUseObject(GameObject gameObject)
    {

        gameObject.SetActive(false);

        var index = using_GameObject.FindIndex(x => int.Equals(x.GetInstanceID() , gameObject.GetInstanceID()));

        using_GameObject.RemoveAt(index);

        stack_GameObject.Push(gameObject);

    } 

    public void AllDelete()
    {
        for(int i =  using_GameObject.Count - 1;  i >= 0; --i)
        {
            stack_GameObject.Push(using_GameObject[i]);

            using_GameObject.RemoveAt(i);   
        }

        

        while(stack_GameObject.Count > 0)
        {
            var go = stack_GameObject.Pop();
            GameObject.Destroy(go);
        }
    }


}