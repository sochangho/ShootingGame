using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{


    private void Awake()
    {
        Created();
    }

    private void OnDestroy()
    {
        Destroyed();
    }

    private void OnEnable()
    {
        Active();
    }

    private void OnDisable()
    {
        InActive();
    }


    abstract public void Created();
    abstract public void Destroyed();
    abstract public void Active();
    abstract public void InActive();



}
