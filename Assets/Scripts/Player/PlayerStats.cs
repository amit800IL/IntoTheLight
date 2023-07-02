using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;

    private void Start()
    {
        playerStats.HP = playerStats.maxHP;
    }

    private void Update()
    {
        if (playerStats.HP >= playerStats.maxHP)
        {
            playerStats.HP = playerStats.maxHP;
        }
        else if (playerStats.HP <= 0)
        {
            playerStats.HP = 0;
        }
    }
}
