using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostEvents : ScriptableObject
{
    public event Action OnGhostAwake;
    public event Action OnGhostSleep;

    public void InvokeGhostAwake()
    {
        OnGhostAwake?.Invoke();
    }

    public void InvokeGhostSleep()
    {
        OnGhostSleep?.Invoke();
    }

    public IDisposable Subscribe(IObserver<GhostEvents> observer)
    {
        throw new NotImplementedException();
    }
}
