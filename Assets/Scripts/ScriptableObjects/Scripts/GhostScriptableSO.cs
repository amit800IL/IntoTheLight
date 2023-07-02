using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostData", menuName = "ScriptableObjects/GhostData")]

public class GhostScriptableSO : ScriptableObject
{
    private List<LightGhost> ghosts = new List<LightGhost>();

    public LightGhost activeGhost;

    private void OnEnable()
    {
        LightGhost[] lightGhosts = FindObjectsOfType<LightGhost>();
        ghosts.AddRange(lightGhosts);
    }


    public void SetGhostAwake(LightGhost ghost)
    {
        foreach (LightGhost g in ghosts)
        {

            if (g != ghost)
            {
                g.OnGhostSleep();
            }
            else
            {
                g.OnGhostAwake();
                activeGhost = g;
            }

        }

    }

    public void ResetGhost()
    {
        activeGhost = null;
    }


    public LightGhost GetActiveGhost()
    {
        if (activeGhost != null && activeGhost.IsGhostAwake)
        {
            return activeGhost;
        }

        return null;
    }

}
