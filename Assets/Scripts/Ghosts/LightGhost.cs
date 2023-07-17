using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LightGhost : MonoBehaviour, IInput
{

    [field: Header("General")]
    public bool IsGhostAwake { get; private set; } = false;
    private PlayerGhostAwake PlayerGhostAwake;
    [SerializeField] private Light Light;
    [SerializeField] private ParticleSystem GhostHealingLight;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private float coolDown;

    [Header("Coroutines")]
    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;
    private Coroutine WakeCourtine;


    private void Start()
    {
        PlayerGhostAwake = GameManager.Instance.PlayerGhostAwake;
        decayCourtuine = StartCoroutine(HealthDownGrduadly());
        CheckIfGhostAwake();
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

    private void CheckIfGhostAwake()
    {
        if (!IsGhostAwake && !PlayerGhostAwake.HasAwaknedGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f && !PlayerGhostAwake.HasAwaknedGhost && PlayerGhostAwake.isInRangeOfGhost)
        {

            OnGhostAwake();

            StopCoroutine(decayCourtuine);
            decayCourtuine = null;

            if (healingCourtuine != null)
            {
                StopCoroutine(healingCourtuine);
                healingCourtuine = null;
            }

            healingCourtuine = StartCoroutine(HealthUpGrduadly());

            if (WakeCourtine != null)
            {
                StopCoroutine(WakeCourtine);
                WakeCourtine = null;
            }

            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }

    public void OnGhostAwake()
    {
        IsGhostAwake = true;

        PlayerGhostAwake.HasAwaknedGhost = true;

        PlayerVoiceManager.Instance.GuardGettingCloser.Stop();
        PlayerVoiceManager.Instance.PlayerOhNoScream.Stop();

        PlayerGhostAwake.playerHealingEffect.Play();
        GhostHealingLight.Play();
    }

    public void OnGhostSleep()
    {

        GhostHealingLight.Stop();
        Light.spotAngle = 40f;
        Light.intensity = 40f;
        PlayerGhostAwake.playerHealingEffect.Stop();
        if (decayCourtuine == null)
        {
            StartCoroutine(HealthDownGrduadly());
        }
    }

    private IEnumerator GhostFromWakeToSleep()
    {
        coolDown = 7f;
        while (coolDown > 0f)
        {
            coolDown -= Time.deltaTime;
            yield return null;

            if (!PlayerGhostAwake.isInRangeOfGhost)
            {
                break;
            }

        }

        OnGhostSleep();

        yield return new WaitForSeconds(coolDown = 7f);

        IsGhostAwake = false;
        PlayerGhostAwake.HasAwaknedGhost = false;
    }

    public IEnumerator HealthUpGrduadly()
    {
        while (GameManager.Instance.playerStats.HP < GameManager.Instance.playerStats.maxHp && PlayerGhostAwake.isInRangeOfGhost)
        {
            GameManager.Instance.playerStats.HP += PlayerGhostAwake.SanityUpNumber;

            yield return new WaitForSeconds(1);
        }

    }
    public IEnumerator HealthDownGrduadly()
    {
        while (GameManager.Instance.playerStats.HP > 0)
        {
            GameManager.Instance.playerStats.HP -= PlayerGhostAwake.SanityDownNumber;

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