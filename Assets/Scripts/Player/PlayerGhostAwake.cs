using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour, IInteractor
{
    private bool isInRangeOfGhost = false;
    private Coroutine healingCoroutine;
    [SerializeField] public bool HasPlayerAwakenGhost = false;
    private Coroutine decayCoroutine;
    private bool isHealingActive = false;
    [SerializeField] private PlayerEvents playerEvents;
    [SerializeField] private GhostEvents ghostEvents;
    [SerializeField] private CoolDownSO cooldownSettings;
    [SerializeField] private PlayerStatsSO PlayerStats;
    [SerializeField] private PlayerVoiceSO PlayerVoice;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private ParticleSystem playerHealingEffect;

    private void Start()
    {
        InputActions.Enable();
        InputActions.Interaction.Enable();
        playerEvents.OnPlayerHeal += OnPlayerHeal;
        InputActions.Interaction.performed += OnInteraction;
        decayCoroutine = StartCoroutine(DecayHealth());
    }

    private void OnDestroy()
    {
        playerEvents.OnPlayerHeal -= OnPlayerHeal;
        InputActions.Interaction.performed -= OnInteraction;
        InputActions.Disable();
        InputActions.Interaction.Disable();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GhostLight"))
        {
            isInRangeOfGhost = true;
            StartHealing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GhostLight"))
        {
            isInRangeOfGhost = false;
            StopHealing();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!HasPlayerAwakenGhost && isInRangeOfGhost && !isHealingActive)
        {
            GameManager.Instance.GhostManager.SetGhostAwake(null);
            HasPlayerAwakenGhost = true;
            StopCoroutine(decayCoroutine);
            LightGhost ghost = GameManager.Instance.GhostManager.GetActiveGhost();
            GameManager.Instance.GhostManager.SetGhostAwake(ghost);

            playerHealingEffect.Play();
            OnPlayerHeal();
            isHealingActive = true;
        }
        else
        {
            isHealingActive = false;
            GameManager.Instance.GhostManager.ResetGhost();
        }
    }

    private void StartHealing()
    {
        if (healingCoroutine == null)
        {
            healingCoroutine = StartCoroutine(HealPlayer());
        }
    }

    private void StopHealing()
    {
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
            playerHealingEffect.Stop();
            StartCoroutine(DecayHealth());
        }
    }

    private IEnumerator HealPlayer()
    {
        while (isInRangeOfGhost && HasPlayerAwakenGhost)
        {
            playerEvents.InvokePlayerHeal();
            yield return new WaitForSeconds(cooldownSettings.CoolDownDuration);

            if (!isHealingActive)
            {
                break;
            }
        }

        HasPlayerAwakenGhost = false;
        StartCoroutine(RegenerateHealth());
    }

    private IEnumerator RegenerateHealth()
    {
        while (PlayerStats.HP < PlayerStats.maxHP && isInRangeOfGhost)
        {
            PlayerStats.HP += 3f;
            yield return null;
        }
    }

    private IEnumerator DecayHealth()
    {
        while (PlayerStats.HP > 0)
        {
            PlayerStats.HP -= 3f;

            if (PlayerStats.HP <= 15f)
            {
                PlayerVoice.playerBreathing.pitch = 2f;
            }

            if (PlayerStats.HP <= 5f)
            {
                PlayerVoice.playerBreathing.Stop();
                PlayerVoice.playerScream.Play();
                PlayerVoice.secondPlayerScream.Play();

                yield return new WaitForSeconds(1f);

                if (PlayerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void OnPlayerHeal()
    {
        if (!isHealingActive)
        {
            isHealingActive = true;
            StartCoroutine(HealPlayer());
        }
    }


}
