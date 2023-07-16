using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour, Iinteraction, IInput
{
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get; private set; } = false;

    [Header("General")]

    private LightGhost ghost;
    private bool shouldHeal;
    [SerializeField] private CoolDownSO coolDown;
    [SerializeField] private ParticleSystem playerHealingEffect;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private GhostEvents ghostEvents;

    [Header("Coroutines")]

    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    [Header("Health Up and Down")]

    [SerializeField] private float SanityUpNumber;
    [SerializeField] private float SanityDownNumber;

    private InputAction.CallbackContext context;

    private void Start()
    {
        context = new InputAction.CallbackContext();
        decayCourtuine = StartCoroutine(HealthDownGrduadly());
    }


    private void OnEnable()
    {
        InputActions.Interaction.performed += OnInteraction;
        InputActions.Interaction.canceled += OnInteraction;
    }

    private void OnDisable()
    {
        InputActions.Interaction.performed -= OnInteraction;
        InputActions.Interaction.canceled -= OnInteraction;
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
            ghost = null;
            isInRangeOfGhost = false;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && healingCourtuine == null && !HasAwaknedGhost && isInRangeOfGhost)
        {
            HasAwaknedGhost = true;

            PlayerVoiceManager.Instance.GuardGettingCloser.Stop();
            PlayerVoiceManager.Instance.PlayerOhNoScream.Stop();

            HealingTimer();

            playerHealingEffect.Play();
            healingCourtuine = StartCoroutine(HealthUpGrduadly());
            StopCoroutine(decayCourtuine);
            decayCourtuine = null;
        }

    }

    private void HealingTimer()
    {
        float coundDown = coolDown.coolDownTimer;
        while (coundDown > 0f)
        {
            coundDown -= Time.deltaTime;
            Debug.Log(coolDown.coolDownTimer);

            if (!isInRangeOfGhost)
            {
                shouldHeal = false;
                break;
            }

        }

        playerHealingEffect.Stop();

        if (healingCourtuine != null)
        {
            StopCoroutine(healingCourtuine);
            healingCourtuine = null;
        }
    }
    public IEnumerator CheckPlayerInput(Collider other)
    {
        shouldHeal = true;

        while (isInRangeOfGhost && shouldHeal)
        {
            OnInteraction(context);
            yield return null;
        }

        if (decayCourtuine == null)
        {
            playerHealingEffect.Stop();
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }

        HasAwaknedGhost = false;
        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {
        while (GameManager.Instance.playerStats.HP < GameManager.Instance.playerStats.maxHp && isInRangeOfGhost)
        {
            GameManager.Instance.playerStats.HP += SanityUpNumber;

            yield return new WaitForSeconds(1);
        }

    }
    private IEnumerator HealthDownGrduadly()
    {
        while (GameManager.Instance.playerStats.HP > 0)
        {
            GameManager.Instance.playerStats.HP -= SanityDownNumber;

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