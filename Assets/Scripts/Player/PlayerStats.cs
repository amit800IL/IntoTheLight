using UnityEngine;

public class PlayerStats : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private PlayerStatsSO playerStats;
=======
    public float HP;
    public float maxHp = 100;
    public float speed;
    public float lookSpeed;
>>>>>>> Fixes

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
