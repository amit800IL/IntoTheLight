using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    private Image HealthBar;
    [HideInInspector] public float CurHealth;
    [HideInInspector] public float MaxHealth = 100f;

    private void Start()
    {
        HealthBar = GetComponent<Image>();
    }
    private void Update()
    {
        CurHealth = GameManager.Instance.playerStats.HP;
        HealthBar.fillAmount = CurHealth / GameManager.Instance.playerStats.maxHp;
    }
}
