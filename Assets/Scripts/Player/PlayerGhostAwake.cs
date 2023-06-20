using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour
{
    public bool isInRangeOfGhost { get; private set; } = false;

    [HideInInspector] public bool HasAwaknedGhost { get; set; } = false;

    [Header("General")]

    [SerializeField] private ParticleSystem particle;
    private bool keypress;

    [field: Header("Audio Sources Refernces")]
    [field: SerializeField] public AudioSource playerBreathing { get; private set; }
    [field: SerializeField] public AudioSource playerScream { get; private set; }
    [field: SerializeField] public AudioSource secondPlayerScream { get; private set; }

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
    public IEnumerator CheckPlayerInput(Collider other)
    {
        bool shouldHeal = true;

        while (isInRangeOfGhost && shouldHeal)
        {

            if (keypress && healingCourtuine == null && !HasAwaknedGhost)
            {
                particle.Play();
                healingCourtuine = StartCoroutine(HealthUpGrduadly());
                StopCoroutine(decayCourtuine);
                decayCourtuine = null;
                float coundDown = 7f;
                float elapsedTime = 0f;
                while (elapsedTime < coundDown)
                {
                    elapsedTime += Time.deltaTime;  
                    yield return null;

                    if (!isInRangeOfGhost)
                    {
                        shouldHeal = false;
                        break;
                    }

                }
                particle.Stop();
                StopCoroutine(healingCourtuine);
                healingCourtuine = null;

                HasAwaknedGhost = true;
            }
            yield return null;
        }

        shouldHeal = false;

        if (decayCourtuine == null)
        {
            particle.Stop();
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }
        yield return new WaitForSeconds(5);
        HasAwaknedGhost = false;
        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {
        while (GameManager.Instance.PlayerStats.HP < GameManager.Instance.PlayerStats.maxHp && isInRangeOfGhost)
        {
            GameManager.Instance.PlayerStats.HP += SanityUpNumber;
            playerBreathing.volume -= 0.1f;
            if (playerBreathing.volume == 0.5f)
            {
                playerBreathing.volume = 0.5f;
            }
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator HealthDownGrduadly()
    {
        while (GameManager.Instance.PlayerStats.HP > 0)
        {
            GameManager.Instance.PlayerStats.HP -= SanityDownNumber;
            playerBreathing.volume += 0.1f;

            if (playerBreathing.volume > 0.5f)
            {
                playerBreathing.volume = 0.5f;
            }

            if (GameManager.Instance.PlayerStats.HP <= 15f)
            {
                playerBreathing.pitch = 2f;
                playerBreathing.volume = 1f;
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsRunning", false);
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsWalking", false);
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsStunned", true);
            }

            if (GameManager.Instance.PlayerStats.HP <= 5f)
            {
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", true);
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsRunning", false);
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsWalking", false);
                GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsStunned", false);
                Camera.main.transform.LookAt(transform.position);
                playerBreathing.Stop();
                playerScream.Play();
                secondPlayerScream.Play();

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