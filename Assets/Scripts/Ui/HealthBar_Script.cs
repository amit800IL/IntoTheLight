using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    private Image HealthBar;
    [HideInInspector] public float CurHealth;
    [HideInInspector] public float MaxHealth = 100f;
<<<<<<< HEAD
    [SerializeField] private PlayerStatsSO playerStats;

=======
>>>>>>> Fixes

    private void Start()
    {
        HealthBar = GetComponent<Image>();
    }
    private void Update()
    {
<<<<<<< HEAD
        CurHealth = playerStats.HP;
        HealthBar.fillAmount = CurHealth / playerStats.maxHP;
=======
        CurHealth = GameManager.Instance.playerStats.HP;
        HealthBar.fillAmount = CurHealth / GameManager.Instance.playerStats.maxHp;
>>>>>>> Fixes
    }
}
