using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    private Image HealthBar;
    public float CurHealth;
    public float MaxHealth = 100f;
    PlayerStats stats;


    private void Start()
    {
        HealthBar = GetComponent<Image>();
        stats = FindObjectOfType<PlayerStats>();
    }
    private void Update()
    {
        CurHealth = stats.HP;
        MaxHealth = stats.maxHp;
        HealthBar.fillAmount = CurHealth / MaxHealth;
    }
}
