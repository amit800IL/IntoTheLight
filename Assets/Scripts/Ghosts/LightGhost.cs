using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour, IInput
{
    private bool IsGhostAwake = false;
    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private ParticleSystem GhostHealingLight;

    [SerializeField] private InputActionsSO InputActions;

    [SerializeField] private CoolDownSO coolDown;

    [SerializeField] private GhostEvents ghostEvents;


    private Coroutine WakeCourtine;
    private void Start()
    {
        ghostEvents.OnGhostAwake += OnGhostAwake;
        ghostEvents.OnGhostSleep += OnGhostSleep;
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
        if (!IsGhostAwake && !GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f && !GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            IsGhostAwake = true;
            Debug.Log("Light ghost interacted");
            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }


    public void OnGhostAwake()
    {
        GhostHealingLight.Stop();
        GhostHealingLight.Clear();
        GhostHealingLight.Play();
        Debug.Log("Light ghost awake");
    }

    public void OnGhostSleep()
    {
        if (WakeCourtine != null)
        {
            StopCoroutine(WakeCourtine);
            WakeCourtine = null;
        }
        GhostHealingLight.Stop();
        Debug.Log("Light ghost asleep");
        Light.spotAngle = 40f;
        Light.intensity = 40f;
        IsGhostAwake = false;
    }

    public IEnumerator GhostFromWakeToSleep()
    {
        OnGhostAwake();
        yield return new WaitForSeconds(coolDown.coolDownTimer);
        OnGhostSleep();
    }

}


