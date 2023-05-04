using System.Collections;
using System.Collections.Generic;
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
        stats=FindObjectOfType<PlayerStats>();
    }
    private void Update()
    {
        CurHealth = stats.HP;
        HealthBar.fillAmount = CurHealth / MaxHealth;
    }
}
