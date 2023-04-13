using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGhostAwake : MonoBehaviour
{
    [SerializeField] private Ghost ghost;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
            ghost.OnPlayerAwakeGhost();
    }


}
