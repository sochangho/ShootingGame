using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents
{
    private List<System.Action> startActions;

    private List<System.Action> endActions;


    public void GameStart()
    {
        if(startActions == null)
        {
            startActions = new List<System.Action>();
        }


        foreach(var e in startActions)
        {
            e?.Invoke();
        }

    }


    public void GameEnd()
    {
        if(endActions == null)
        {
            endActions = new List<System.Action>();
        }


        foreach (var e in endActions)
        {
            e?.Invoke();
        }

    }


    public void AddStartEvent(System.Action action)
    {
        if (startActions == null)
        {
            startActions = new List<System.Action>();
        }

        startActions.Add(action);
    }

    public void AddEndEvent(System.Action action)
    {
        if (endActions == null)
        {
            endActions = new List<System.Action>();
        }

        endActions.Add(action);
    }

    public void AllDelete()
    {
        startActions = null;
        endActions = null;
    }
}
