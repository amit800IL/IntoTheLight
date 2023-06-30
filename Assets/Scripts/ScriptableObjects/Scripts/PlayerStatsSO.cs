using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] public float HP;
    [SerializeField] public float maxHP;
    [SerializeField] public float speed;
    [SerializeField] public float lookSpeed;
}
