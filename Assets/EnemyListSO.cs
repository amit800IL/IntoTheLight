using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "ScriptableObjects/EnemyList")]
public class EnemyListSO : ScriptableObject
{
   public List<GameObject> enemyList = new List<GameObject>();    
}