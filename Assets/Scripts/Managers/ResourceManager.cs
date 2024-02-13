using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AssetType
{
    Data,

    Prefab,

    Sprite,

    Size
}


public class ResourceManager
{
    static readonly string[] s_extPrefab = new string[1] { ".prefab" };
    static readonly string[] s_extAudio = new string[3] { ".mp3", ".wav", ".ogg" };
    static readonly string[] s_extTexture = new string[1] { ".png" };
    static readonly string[] s_extText = new string[2] { ".bytes", ".txt" };

    static Dictionary<string, Object>[] dicResources;



    static ResourceManager()
    {
        if (dicResources == null)
        {

            dicResources = new Dictionary<string, Object>[(int)AssetType.Size];
            for (int i = 0; i < (int)AssetType.Size; ++i)
            {
                dicResources[i] = new Dictionary<string, Object>();
            }
        }
    }




    static public T LoadData<T>() where T : ScriptableObject
    {
        string path = $"Tables/{typeof(T).Name}";

        T loadData = default(T);

        Debug.Log($"Load Path {path}");

        if (dicResources[(int)AssetType.Data].ContainsKey(path))
        {
            loadData = (T)dicResources[(int)AssetType.Data][path];

        }
        else
        {
            loadData =   Resources.Load<T>(path);
            dicResources[(int)AssetType.Data].Add(path, loadData);

        }
        

        if(loadData == null)
        {
            Debug.Log($"<color=red> Fail {path} </color>");
        }
        else
        {
            Debug.Log($"<color=blue> Success {path} </color>");
        }


        return loadData;
    }


    static public T Load<T>(AssetType assetType  , string resourcespath) where T : Object
    {
        T load = default(T);

        if (dicResources[(int)assetType].ContainsKey(resourcespath))
        {
            load = (T)dicResources[(int)assetType][resourcespath];

        }
        else
        {
            load = Resources.Load<T>(resourcespath);
            dicResources[(int)assetType].Add(resourcespath, load);
        }

        if (load == null)
        {
            Debug.Log($"<color=red> Fail {resourcespath} </color>");
        }
        else
        {
            Debug.Log($"<color=blue> Success {resourcespath} </color>");
        }


        return load;
    }
    

    /// <summary>
    ///  나중에 아틀라스로 변경
    /// </summary>
    /// <param name="resourcespath"></param>
    /// <returns></returns>
    static public Sprite SpriteLoad(string resourcespath)
    {
        return  Load<Sprite>(AssetType.Sprite, resourcespath);
    }

 


}


public class PathString
{


    public const string TABLE = "Resources/Tables";
    public const string PREFAB_PLAYER = "Prefabs/Character/Players";
    public const string PREFAB_PROJECTILE = "Prefabs/Projectile";
    public const string PREFAB_ENEMY = "Prefabs/Character/Enemys";
    public const string PREFAB_ELEMENTS = "Prefabs/UI/Elements";
    public const string PREFAB_EFFECT = "Prefabs/Effect";

    public const string PREFABS_ITEM_WEAPON = "Prefabs/Item/Weapon";

    public const string POPUP = "Prefabs/UI/Popup";

    public const string PROGRESS = "Prefabs/UI/Progress";



    public const string ITEM_SPRITE_ABILITY = "Sprites/Item/Ability";   
    public const string ITEM_SPRITE_WEAPON = "Sprites/Item/Weapon";
    public const string ITEM_SPRITE_RECOVERY = "Sprites/Item/Recovery";

}

