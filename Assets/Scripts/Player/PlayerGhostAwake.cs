using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGhostAwake : MonoBehaviour
{
    [SerializeField] private Ghost ghost;

    private void Awake()
    {
        ghost.SetPlayer(this);
    }

    private void Update()
    {
        ghost.OnPlayerAwakeGhost();
    }
}
