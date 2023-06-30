using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour, IInteractor
{
    [SerializeField] private Light Light;
    [SerializeField] private ParticleSystem GhostHealingLight;
    [SerializeField] private PlayerEventsSO PlayerEvents;
    [SerializeField] private GhostScriptableSO ghostScriptable;
    [SerializeField] private GhostEventsSO ghostEvents;
    [SerializeField] private PlayerVoiceSO playerVoiceScriptable;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private CoolDownSO coolDownDuration;

    public bool IsGhostAwake = false;

    private Coroutine WakeCourtine;

    private void Start()
    {
        CheckIfGhostAwake();
        ghostEvents.OnGhostAwake += OnPlayerAwakeGhost;
        PlayerEvents.OnPlayerAwakeGhost += OnPlayerAwakeGhost;
        ghostEvents.OnGhostSleep += OnGhostGoToSleep;
    }

    private void OnDestroy()
    {
        ghostEvents.OnGhostAwake -= OnPlayerAwakeGhost;
        PlayerEvents.OnPlayerAwakeGhost -= OnPlayerAwakeGhost;
        ghostEvents.OnGhostSleep -= OnGhostGoToSleep;
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

    private void CheckIfGhostAwake()
    {
        if (!IsGhostAwake && !ghostScriptable.HasPlayerAwakenGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (InputActions.Interaction.triggered && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f && !ghostScriptable.HasPlayerAwakenGhost)
        {
            IsGhostAwake = true;
            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }

    public void OnPlayerAwakeGhost()
    {
        PlayerEvents.InvokePlayerHeal();
        GhostHealingLight.Play();
        playerVoiceScriptable.playerOhNoScream.Play();
    }

    public void OnGhostGoToSleep()
    {
        if (WakeCourtine != null)
        {
            StopCoroutine(WakeCourtine);
            WakeCourtine = null;
        }
        GhostHealingLight.Stop();
        GhostHealingLight.gameObject.SetActive(false);
        GhostHealingLight.gameObject.SetActive(true);
        Light.spotAngle = 40f;
        Light.intensity = 40f;
        IsGhostAwake = false;
    }

    public IEnumerator GhostFromWakeToSleep()
    {
        PlayerEvents.InvokePlayerAwakeGhost();
        ghostEvents.InvokeGhostAwake();
        yield return new WaitForSeconds(coolDownDuration.CoolDownDuration);
        ghostEvents.InvokeGhostSleep();
    }


}


