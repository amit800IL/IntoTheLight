using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private ParticleSystem particle;
    private bool keypress;
    private bool isInRangeOfGhost = false;

    [Header("Coroutines")]
    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    [Header("Health Up and Down")]
    [SerializeField] private float SanityUpNumber;
    [SerializeField] private float SanityDownNumber;


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
                particle.Play();
                healingCourtuine = StartCoroutine(HealthUpGrduadly());
                StopCoroutine(decayCourtuine);
                decayCourtuine = null;
                yield return new WaitForSeconds(10);
                particle.Stop();
            }

            yield return null;
        }

        StopCoroutine(healingCourtuine);
        healingCourtuine = null;

        if (decayCourtuine == null)
        {
            particle.Stop();
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }

        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {
        while (GameManager.Instance.PlayerStats.HP < GameManager.Instance.PlayerStats.maxHp)
        {
            GameManager.Instance.PlayerStats.HP += SanityUpNumber;
            GameManager.Instance.playerBreathing.volume -= 0.1f;
            if (GameManager.Instance.playerBreathing.volume == 0.5f)
            {
                GameManager.Instance.playerBreathing.volume = 0.5f;
            }
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator HealthDownGrduadly()
    {
        while (GameManager.Instance.PlayerStats.HP > 0)
        {
            GameManager.Instance.PlayerStats.HP -= SanityDownNumber;
            GameManager.Instance.playerBreathing.volume += 0.1f;

            if (GameManager.Instance.playerBreathing.volume == 0.5f)
            {
                GameManager.Instance.playerBreathing.volume = 1f;
            }

            if (GameManager.Instance.PlayerStats.HP < 10f)
            {
                GameManager.Instance.playerScream.Play();
                GameManager.Instance.secondPlayerScream.Play();
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", true);
                GameManager.Instance.playerBreathing.Stop();

                yield return new WaitForSeconds(1);

                if (GameManager.Instance.PlayerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}