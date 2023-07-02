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
<<<<<<< HEAD
=======
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Coroutines")]

    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    [Header("Health Up and Down")]

    [SerializeField] private float SanityUpNumber;
    [SerializeField] private float SanityDownNumber;
>>>>>>> Fixes

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
<<<<<<< HEAD
=======
            ghost.OnGhostSleep();
            ghost = null;
>>>>>>> Fixes
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
<<<<<<< HEAD
                break;
=======
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

>>>>>>> Fixes
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
<<<<<<< HEAD
=======

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

>>>>>>> Fixes
    }

    private IEnumerator DecayHealth()
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> Fixes
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
