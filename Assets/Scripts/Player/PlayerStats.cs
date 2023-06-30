using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsScriptable;
    [HideInInspector] public float HP;
    [HideInInspector] public float maxHp = 100;

    private void Start()
    {
        playerStatsScriptable.maxHP = maxHp;
        playerStatsScriptable.HP = HP;
    }
    private void Update()
    {
        if (playerStatsScriptable.HP >= 100)
        {
            playerStatsScriptable.maxHP = maxHp;
        }
        else if (playerStatsScriptable.HP <= 0)
        {
            playerStatsScriptable.HP = 0;
        }
    }
}
