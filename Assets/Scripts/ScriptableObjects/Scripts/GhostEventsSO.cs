using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GhostsEvents", menuName = "ScriptableObjects/GhostsEvents")]

public class GhostEventsSO : ScriptableObject
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
}
