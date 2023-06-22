using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public float HP;
    [HideInInspector] public float maxHp = 100;

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
