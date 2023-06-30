
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]

public class EnemyStatsSO : ScriptableObject
{
    [SerializeField] public float speedMultiplayer;
    [SerializeField] public float KillingDistance;
    [SerializeField] public bool canKillPlayer;

}
