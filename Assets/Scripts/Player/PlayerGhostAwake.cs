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
            ghost.OnGhostSleep();
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
                PlayerVoiceManager.Instance.GuardGettingCloser.Stop();
                PlayerVoiceManager.Instance.PlayerOhNoScream.Stop();
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
        while (GameManager.Instance.playerStats.HP < GameManager.Instance.playerStats.maxHp && isInRangeOfGhost)
        {
            GameManager.Instance.playerStats.HP += SanityUpNumber;

            if (PlayerVoiceManager.Instance.playerBreathing.volume == 0.5f)
            {
                PlayerVoiceManager.Instance.playerBreathing.volume = 1f;
                PlayerVoiceManager.Instance.playerBreathing.volume -= 0.1f;
            }
            yield return new WaitForSeconds(1);
        }

    }

    private IEnumerator HealthDownGrduadly()
    {
        while (GameManager.Instance.playerStats.HP > 0)
        {
            GameManager.Instance.playerStats.HP -= SanityDownNumber;

            if (PlayerVoiceManager.Instance.playerBreathing.volume > 0.5f)
            {
                PlayerVoiceManager.Instance.playerBreathing.volume = 1f;
            }

            if (GameManager.Instance.playerStats.HP <= 15f)
            {
                PlayerVoiceManager.Instance.playerBreathing.pitch = 2f;
                PlayerVoiceManager.Instance.playerBreathing.volume = 1f;
            }   

            if (GameManager.Instance.playerStats.HP <= 5f)
            {
              
                Camera.main.transform.LookAt(transform.position);
                PlayerVoiceManager.Instance.playerBreathing.Stop();
                PlayerVoiceManager.Instance.playerScream.Play();
                PlayerVoiceManager.Instance.secondPlayerScream.Play();

                yield return new WaitForSeconds(1);

                if (GameManager.Instance.playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

}