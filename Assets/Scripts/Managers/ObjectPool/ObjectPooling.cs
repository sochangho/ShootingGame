using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public int Id { get; private set; }
    public GameObjectType Type {get; private set;}


    public void Destroy()
    {
        ObjectPoolManager.Instance.UnUseObject(this);
    }

    public void Set(int id , GameObjectType type)
    {
        Id = id;
        Type = type;
    }




}
