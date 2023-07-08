using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float HP;
    public float maxHp = 100;
    public float speed;
    public float lookSpeed;

    private void Start()
    {
        HP = maxHp;
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
