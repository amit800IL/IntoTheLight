using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour, IPlayerInput, IInteractor
{
    private bool isInRangeOfGhost = false;
    private Coroutine healingCoroutine;
    private Coroutine decayCoroutine;
    [SerializeField] private PlayerEventsSO playerEvents;
    [SerializeField] private GhostEventsSO ghostEvents;
    [SerializeField] private CoolDownSO cooldownSettings;
    [SerializeField] private PlayerStatsSO PlayerStats;
    [SerializeField] private PlayerVoiceSO PlayerVoice;
    [SerializeField] private GhostScriptableSO ghostScriptable;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private ParticleSystem playerHealingEffect;

    private void Start()
    {
        playerEvents.OnPlayerHeal += OnPlayerHeal;
        decayCoroutine = StartCoroutine(HealthDownGrduadly());
    }



    private void OnDestroy()
    {
        playerEvents.OnPlayerHeal -= OnPlayerHeal;
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Interaction.performed += OnInteraction;
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Interaction.performed -= OnInteraction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            LightGhost ghost = ghostScriptable.GetLightGhost();

            if (ghost != null)
            {
                isInRangeOfGhost = true;
                StartCoroutine(CheckPlayerInput(other, ghost));
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            LightGhost ghost = ghostScriptable.GetLightGhost();
            if (ghost != null)
            {
                ghost.OnGhostGoToSleep();
            }
            isInRangeOfGhost = false;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (InputActions.Interaction.triggered && healingCoroutine == null && !ghostScriptable.HasPlayerAwakenGhost)
        {
            ghostScriptable.HasPlayerAwakenGhost = true;
            healingCoroutine = StartCoroutine(HealthUpGrduadly());
            playerHealingEffect.Play();
            playerEvents.InvokePlayerAwakeGhost();
            playerEvents.InvokePlayerHeal();
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(cooldownSettings.CoolDownDuration);
        StopCoroutine(decayCoroutine);
        decayCoroutine = null;
    }
    public IEnumerator CheckPlayerInput(Collider other = null, LightGhost ghost = null)
    {
        bool shouldHeal = true;

        while (isInRangeOfGhost && shouldHeal)
        {
            StartCooldown();

            yield return null;

            if (decayCoroutine == null)
            {
                playerHealingEffect.Stop();
                decayCoroutine = StartCoroutine(HealthDownGrduadly());
            }

            InputAction.CallbackContext interactionContext = new();
            OnInteraction(interactionContext);


            yield return new WaitForSeconds(cooldownSettings.CoolDownDuration);
            ghostScriptable.HasPlayerAwakenGhost = false;

            shouldHeal = isInRangeOfGhost;

            yield return null;
        }

        shouldHeal = false;

        if (decayCoroutine == null)
        {
            playerHealingEffect.Stop();
            decayCoroutine = StartCoroutine(HealthDownGrduadly());
        }

        yield return new WaitForSeconds(5);
        ghostScriptable.HasPlayerAwakenGhost = false;
        yield return null;

    }
    private IEnumerator HealthUpGrduadly()
    {

        while (PlayerStats.HP < PlayerStats.maxHP && isInRangeOfGhost)
        {
            PlayerStats.HP += 3f;
            playerEvents.InvokePlayerHeal();

            if (PlayerVoice.playerBreathing.volume == 0.5f)
            {
                PlayerVoice.playerBreathing.volume = 1f;
                PlayerVoice.playerBreathing.volume -= 0.1f;
            }

            yield return null;
        }
    }

    private void OnPlayerHeal()
    {
        healingCoroutine = StartCoroutine(HealthUpGrduadly());
    }
    private IEnumerator HealthDownGrduadly()
    {
        while (PlayerStats.HP > 0)
        {
            PlayerStats.HP -= 3f;

            if (PlayerVoice.playerBreathing.volume > 0.5f)
            {
                PlayerVoice.playerBreathing.volume = 1f;
            }

            if (PlayerStats.HP <= 15f)
            {
                PlayerVoice.playerBreathing.pitch = 2f;
                PlayerVoice.playerBreathing.volume = 1f;
            }

            if (PlayerStats.HP <= 5f)
            {
                Camera.main.transform.LookAt(transform.position);
                PlayerVoice.playerBreathing.Stop();
                PlayerVoice.playerScream.Play();
                PlayerVoice.secondPlayerScream.Play();

                yield return new WaitForSeconds(1);

                if (PlayerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }


}