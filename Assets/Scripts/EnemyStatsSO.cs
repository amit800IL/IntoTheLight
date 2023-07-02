using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] public float EnemySpeed;
    [SerializeField] public float killingDistance;
    [SerializeField] public bool canKillPlayer;
}
