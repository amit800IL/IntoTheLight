using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private bool keypress;
    private bool isInRangeOfGhost = false;
    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;


    private void Start()
    {
        decayCourtuine = StartCoroutine(HealthDownGrduadly());

    }

    private void Update()
    {
        keypress = Keyboard.current.fKey.isPressed;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("GhostLight"))
        {
            isInRangeOfGhost = true;
            StartCoroutine(CheckPlayerInput(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            isInRangeOfGhost = false;
        }
    }

    private IEnumerator CheckPlayerInput(Collider other)
    {
        while (isInRangeOfGhost)
        {
            if (keypress && healingCourtuine == null)
            {
                healingCourtuine = StartCoroutine(HealthUpGrduadly());
                StopCoroutine(decayCourtuine);
                decayCourtuine = null;
            }

            yield return null;
        }

        StopCoroutine(healingCourtuine);
        healingCourtuine = null;

        if (decayCourtuine == null)
        {
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }

        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {
        while (playerStats.HP < playerStats.maxHp)
        {
            playerStats.HP += 8f;
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator HealthDownGrduadly()
    {
        while (playerStats.HP > 0)
        {
            playerStats.HP -= 8f;

            yield return new WaitForSeconds(1);
        }
    }


}
