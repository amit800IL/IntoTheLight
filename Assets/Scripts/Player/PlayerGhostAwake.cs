using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour, IInput
{

    [field: Header("General")]
    [field: SerializeField] public ParticleSystem playerHealingEffect { get; private set; }

    [SerializeField] private InputActionsSO InputActions;
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get => hasAwaknedGhost; set => hasAwaknedGhost = value; }

    private bool hasAwaknedGhost;

    private LightGhost ghost;

    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    [Header("Health Up and Down")]

    [SerializeField] public float SanityUpNumber;
    [SerializeField] public float SanityDownNumber;

    private void Start()
    {
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
            ghost = other.GetComponent<LightGhost>();
            isInRangeOfGhost = true;

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
        if (context.performed && isInRangeOfGhost && !hasAwaknedGhost && ghost.coolDown == ghost.elapsedTime)
        {
            ghost.OnGhostInteraction();

            HasAwaknedGhost = true;

            StopCoroutine(decayCourtuine);
            decayCourtuine = null;

            if (healingCourtuine != null)
            {
                StopCoroutine(healingCourtuine);
                healingCourtuine = null;
            }

            healingCourtuine = StartCoroutine(HealthUpGrduadly());

            playerHealingEffect.Play();
        }
    }
    public IEnumerator HealthUpGrduadly()
    {
        while (GameManager.Instance.playerStats.HP < GameManager.Instance.playerStats.maxHp && isInRangeOfGhost)
        {
            GameManager.Instance.playerStats.HP += SanityUpNumber;

            yield return new WaitForSeconds(1);
        }

    }
    public IEnumerator HealthDownGrduadly()
    {
        playerHealingEffect.Stop();
        hasAwaknedGhost = false;

        while (GameManager.Instance.playerStats.HP > 0)
        {
            GameManager.Instance.playerStats.HP -= SanityDownNumber;

            if (GameManager.Instance.playerStats.HP <= 5f)
            {

                Camera.main.transform.Rotate(0, 0, 0);
                PlayerVoiceManager.Instance.playerBreathing.Stop();
                PlayerVoiceManager.Instance.playerScream.Play();
                PlayerVoiceManager.Instance.secondPlayerScream.Play();

                InputActions.Move.Disable();
                InputActions.Look.Disable();
                yield return new WaitForSeconds(1);

                if (GameManager.Instance.playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            Debug.Log("Player lost health");
            yield return new WaitForSeconds(1);
        }
    }
    public void StartDecay()
    {
        if (healingCourtuine != null)
        {
            StopCoroutine(healingCourtuine);
            healingCourtuine = null;
        }
        if (decayCourtuine == null)
        {
            decayCourtuine = StartCoroutine(HealthDownGrduadly());
        }
    }

}