using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour, IInput
{
    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private ParticleSystem GhostHealingLight;

    public bool IsGhostAwake = false;

    private Coroutine WakeCourtine;

    [SerializeField] private InputActionsSO InputActions;
    private void Start()
    {
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
            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }


    public void OnGhostAwake()
    {
        GhostHealingLight.Play();
    }

    public void OnGhostSleep()
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
        OnGhostAwake();
        yield return new WaitForSeconds(7);
        OnGhostSleep();
    }

}


