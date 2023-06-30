using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GhostData", menuName = "ScriptableObjects/GhostData")]

public class GhostScriptableSO : ScriptableObject
{
    [HideInInspector] public bool HasPlayerAwakenGhost = false;

    [HideInInspector] public bool keyPress = false;

    private List<LightGhost> ghosts = new List<LightGhost>();

    private void OnEnable()
    {
        ghosts.Clear();

        LightGhost[] lightGhosts = FindObjectsOfType<LightGhost>();
        ghosts.AddRange(lightGhosts);
    }

    public LightGhost GetLightGhost()
    {
        foreach (LightGhost ghost in ghosts)
        {
            if (ghost.IsGhostAwake)
            {
                return ghost;
            }
        }

        return null;
    }
}
