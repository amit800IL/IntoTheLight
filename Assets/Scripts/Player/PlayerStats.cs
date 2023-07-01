using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;
    private void Update()
    {
        if (playerStats.HP >= 100)
        {
            playerStats.HP = playerStats.maxHP;
        }
        else if (playerStats.HP <= 0)
        {
            playerStats.HP = 0;
        }
    }
}
