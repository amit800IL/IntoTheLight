using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    private Image HealthBar;
    [HideInInspector] public float CurHealth;
    [HideInInspector] public float MaxHealth = 100f;
    [SerializeField] private PlayerStatsSO playerStats;


    private void Start()
    {
        HealthBar = GetComponent<Image>();
    }
    private void Update()
    {
        CurHealth = playerStats.HP;
        HealthBar.fillAmount = CurHealth / playerStats.maxHP;
    }
}
