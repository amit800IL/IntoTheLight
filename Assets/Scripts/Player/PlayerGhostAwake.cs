using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGhostAwake : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        bool keypress = Keyboard.current.fKey.isPressed;

        if (keypress && other.gameObject.CompareTag("GhostLight"))
        {


            StartCoroutine(HealthUpGrduadly());

        }

        else if (!keypress && !other.gameObject.CompareTag("GhostLight"))
        {


            StartCoroutine(HealthDownGrduadly());

        }
    }

    private IEnumerator HealthUpGrduadly()
    {
        while (playerStats.HP < playerStats.maxHp)
        {
            playerStats.HP += 20f;
            Debug.Log("HP up");
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator HealthDownGrduadly()
    {
        while (playerStats.HP > 0)
        {
            playerStats.HP -= 1.5f;
            Debug.Log("HP down");
            yield return new WaitForSeconds(1);
        }
    }


}
