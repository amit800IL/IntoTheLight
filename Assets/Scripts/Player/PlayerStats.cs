using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HP;
    public float maxHp = 100;
    public float speed;
    public int CurrKeys;

    private void Start()
    {
        HP = maxHp;
        CurrKeys = 0;
    }
    private void Update()
    {
        if (HP >= 100)
        {
            HP = maxHp;
        }
        else if (HP <= 0)
        {
            HP = 0;
        }
    }
}
