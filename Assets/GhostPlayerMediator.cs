using System.Collections.Generic;
using UnityEngine;

public class GhostPlayerMediator : MonoBehaviour, IGhostPlayerMediator
{
    private List<IPlayerHealing> healingObjects = new List<IPlayerHealing>();
    private bool isGhostAwake = false;
    public void OnGhostAwake()
    {
        isGhostAwake = true;

        foreach (IPlayerHealing healingObject in healingObjects)
        {
            healingObject.OnPlayerHealingStart();
        }
    }

    public void OnGhostSleep()
    {
        isGhostAwake = false;

        foreach (IPlayerHealing healingObject in healingObjects)
        {
            healingObject.OnPlayerHealingStop();
        }
    }

    public void RegisterPlayerHealing(IPlayerHealing playerHealing)
    {
        foreach (IPlayerHealing healingObject in healingObjects)
        {
            if (!healingObjects.Contains(playerHealing))
            {
                healingObjects.Add(playerHealing);
            }
        }
    }

    public void UnregisterPlayerHealing(IPlayerHealing playerHealing)
    {
        foreach (IPlayerHealing heaingObject in healingObjects)
        {
            if (!healingObjects.Contains(playerHealing))
            {
                healingObjects.Remove(playerHealing);
            }
        }
    }

    public bool IsGhostAwake()
    {
        return isGhostAwake;
    }

}
