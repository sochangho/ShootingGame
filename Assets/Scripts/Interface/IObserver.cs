using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    void UpdateData(object data);
}


public interface ISubject
{
    void ResisterObserver(IObserver observer);

    void RemoveObserver(IObserver observer);

    void NotifyObserver();
}