using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour, Iinteraction
{
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get; private set; } = false;

    [Header("General")]

    private LightGhost ghost;
    private bool keypress;
    [SerializeField] private ParticleSystem playerHealingEffect;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerVoice playerVoice;
    [SerializeField] private PlayerStats playerStats;

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
        keypress = Keyboard.current.eKey.isPressed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            ghost = other.GetComponentInParent<LightGhost>();
            isInRangeOfGhost = true;
            StartCoroutine(CheckPlayerInput(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            ghost.OnGhostGoToSleep();
            ghost = null;
            isInRangeOfGhost = false;
        }
    }
    public IEnumerator CheckPlayerInput(Collider other)
    {
        bool shouldHeal = true;

        while (isInRangeOfGhost && shouldHeal)
        {
            ghost.transform.rotation = transform.rotation;

            if (keypress && healingCourtuine == null && !HasAwaknedGhost)
            {
                ghost.transform.rotation = transform.rotation;
                HasAwaknedGhost = true;
                playerHealingEffect.Play();
                healingCourtuine = StartCoroutine(HealthUpGrduadly());
                StopCoroutine(decayCourtuine);
                decayCourtuine = null;
                playerVoice.GuardGettingCloser.Stop();
                playerVoice.PlayerOhNoScream.Stop();
                float coundDown = 7f;
                float elapsedTime = 0f;
                while (elapsedTime < coundDown)
                {
                    ghost.transform.rotation = transform.rotation;
                    elapsedTime += Time.deltaTime;
                    yield return null;

                    if (!isInRangeOfGhost)
                    {
                        shouldHeal = false;
                        break;
                    }

                }
                playerHealingEffect.Stop();
                StopCoroutine(healingCourtuine);
                healingCourtuine = null;

            }
            yield return null;
        }

        shouldHeal = false;

        if (decayCourtuine == null)
        {
            playerHealingEffect.Stop();
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }

        yield return new WaitForSeconds(5);
        HasAwaknedGhost = false;
        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {
        while (playerStats.HP < playerStats.maxHp && isInRangeOfGhost)
        {
            playerStats.HP += SanityUpNumber;

            if (playerVoice.playerBreathing.volume == 0.5f)
            {
                playerVoice.playerBreathing.volume = 1f;
                playerVoice.playerBreathing.volume -= 0.1f;
            }
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator HealthDownGrduadly()
    {
        while (playerStats.HP > 0)
        {
            playerStats.HP -= SanityDownNumber;

            if (playerVoice.playerBreathing.volume > 0.5f)
            {
                playerVoice.playerBreathing.volume = 1f;
            }

            if (playerStats.HP <= 15f)
            {
                playerVoice.playerBreathing.pitch = 2f;
                playerVoice.playerBreathing.volume = 1f;
                playerMovement.playerAnimator.SetBool("IsRunning", false);
                playerMovement.playerAnimator.SetBool("IsWalking", false);
                playerMovement.playerAnimator.SetBool("IsStunned", true);
            }

            if (playerStats.HP <= 5f)
            {
                playerMovement.playerAnimator.SetBool("IsAttacked", true);
                playerMovement.playerAnimator.SetBool("IsRunning", false);
                playerMovement.playerAnimator.SetBool("IsWalking", false);
                playerMovement.playerAnimator.SetBool("IsStunned", false);
                Camera.main.transform.LookAt(transform.position);
                playerVoice.playerBreathing.Stop();
                playerVoice.playerScream.Play();
                playerVoice.secondPlayerScream.Play();

                yield return new WaitForSeconds(1);

                if (playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}