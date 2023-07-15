using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LightGhostRefactor : MonoBehaviour, IInput, IPlayerHealing
{
    private bool isGhostAwake = false;

    private IGhostPlayerMediator ghostPlayerMediator;
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get; private set; } = false;
    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private ParticleSystem GhostHealingLight;
    [SerializeField] private ParticleSystem playerHeaingEffect;

    [SerializeField] private InputActionsSO InputActions;

    [SerializeField] private CoolDownSO coolDown;

    [SerializeField] private float SanityUpNumber;
    [SerializeField] private float SanityDownNumber;

    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    private bool shouldHeal = false;

    private void Start()
    {
        decayCourtuine = StartCoroutine(HealthDownGrduadly());
        SetGhostLight();
         ghostPlayerMediator = FindObjectOfType<GhostPlayerMediator>();
        if (ghostPlayerMediator != null)
        {
            SetMediator(ghostPlayerMediator);
            ghostPlayerMediator.RegisterPlayerHealing(this);
        }
        else
        {
            Debug.LogError("GhostPlayerMediator script not found in the scene!");
        }
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
        if (ghostPlayerMediator != null)
        {
            ghostPlayerMediator.UnregisterPlayerHealing(this);
        }
    }
    public void SetMediator(IGhostPlayerMediator mediator)
    {
        this.ghostPlayerMediator = mediator;
    }

    private void SetGhostLight()
    {
        Light.spotAngle = 40f;
        Light.intensity = 40f;
    }
    public void OnInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("is Ghost Awake" + ghostPlayerMediator.IsGhostAwake());
        Debug.Log("is in range" + isInRangeOfGhost);
        if (context.performed && !ghostPlayerMediator.IsGhostAwake() && isInRangeOfGhost)
        {
            shouldHeal = true;
            Debug.Log("GhostInteracted");
            StopCoroutine(decayCourtuine);
            decayCourtuine = null;

            ghostPlayerMediator.OnGhostAwake();
            GhostHealingLight.Play();
            playerHeaingEffect.Play();

        }
    }

    public void OnPlayerHealingStart()
    {
        StartCoroutine(HealthUpGrduadly());
    }

    public void OnPlayerHealingStop()
    {
        StartCoroutine(HealthDownGrduadly());
    }

    private IEnumerator HealingTimer()
    {
        float coundDown = coolDown.coolDownTimer;
        float elapsedTime = 0f;
        while (elapsedTime < coundDown)
        {
            elapsedTime += Time.deltaTime;
            yield return null;

            if (!isInRangeOfGhost)
            {
                break;
            }

        }

        playerHeaingEffect.Stop();

        if (healingCourtuine != null)
        {
            StopCoroutine(healingCourtuine);
            healingCourtuine = null;
        }
    }

    private IEnumerator HealthUpGrduadly()
    {

        while (GameManager.Instance.playerStats.HP < GameManager.Instance.playerStats.maxHp && isInRangeOfGhost)
        {
            GameManager.Instance.playerStats.HP += SanityUpNumber;

            float coundDown = coolDown.coolDownTimer;
            float elapsedTime = 0f;
            while (elapsedTime < coundDown)
            {
                elapsedTime += Time.deltaTime;
                yield return null;

                if (!isInRangeOfGhost)
                {
                    break;
                }

            }

            playerHeaingEffect.Stop();

            if (healingCourtuine != null)
            {
                StopCoroutine(healingCourtuine);
                healingCourtuine = null;
            }


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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeOfGhost = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRangeOfGhost = false;
            OnPlayerHealingStop();
        }
    }

   
}
