using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
   [SerializeField] private List<LightGhost> ghosts = new List<LightGhost>();

    public LightGhost activeGhost;

    public List<LightGhost> Ghosts { get => ghosts; }

    public void AddGhost(LightGhost ghost)
    {
       Ghosts.Add(ghost);   
    }

    public void SetGhostAwake(LightGhost ghost)
    {
        //foreach (LightGhost g in Ghosts)
        //{

        //    if (g != ghost)
        //    {
        //        g.OnGhostSleep();
        //    }
        //    else
        //    {
        //        g.OnGhostAwake();
        //        activeGhost = g;
        //    }

        //}

    }

    public void ResetGhost()
    {
        activeGhost = null;
    }


    //public LightGhost GetActiveGhost()
    //{
    //    //if (activeGhost != null && activeGhost.IsGhostAwake)
    //    //{
    //    //    return activeGhost;
    //    //}

    //    //return null;
    //}
}
