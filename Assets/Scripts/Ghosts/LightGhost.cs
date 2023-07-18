using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour
{

    [field: Header("General")]
    public bool IsGhostAwake { get; private set; } = false;
    private PlayerGhostAwake PlayerGhostAwake;
    [SerializeField] private Light Light;
    [SerializeField] private ParticleSystem GhostHealingLight;
    [SerializeField] private InputActionsSO InputActions;
    public float coolDown = 7f;
    [SerializeField] public float elapsedTime;

    [Header("Coroutines")]
    private Coroutine WakeCourtine;


    private void Start()
    {
        elapsedTime = coolDown;
        PlayerGhostAwake = GameManager.Instance.PlayerGhostAwake;
        CheckIfGhostAwake();
    }

    //private void OnEnable()
    //{
    //    InputActions.Interaction.performed += OnInteraction;
    //    InputActions.Interaction.canceled += OnInteraction;
    //}

    //private void OnDisable()
    //{
    //    InputActions.Interaction.performed -= OnInteraction;
    //    InputActions.Interaction.canceled -= OnInteraction;
    //}

    private void CheckIfGhostAwake()
    {
        if (!IsGhostAwake && !PlayerGhostAwake.HasAwaknedGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    //public void OnInteraction(InputAction.CallbackContext context)
    //{
    //    if (context.performed && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f && !PlayerGhostAwake.HasAwaknedGhost && PlayerGhostAwake.isInRangeOfGhost)
    //    {

    //        OnGhostAwake();

    //        if (WakeCourtine != null)
    //        {
    //            StopCoroutine(WakeCourtine);
    //            WakeCourtine = null;
    //        }


    //        WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
    //    }
    //}

    public void OnGhostInteraction()
    {
        OnGhostAwake();

        if (WakeCourtine != null)
        {
            StopCoroutine(WakeCourtine);
            WakeCourtine = null;
        }

        WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
    }

    public void OnGhostAwake()
    {
        IsGhostAwake = true;

        PlayerVoiceManager.Instance.GuardGettingCloser.Stop();
        PlayerVoiceManager.Instance.PlayerOhNoScream.Stop();

        GhostHealingLight.Play();
    }

    public void OnGhostSleep()
    {
        GhostHealingLight.Stop();
        Light.spotAngle = 40f;
        Light.intensity = 40f;
    }

    private IEnumerator GhostFromWakeToSleep()
    {
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;

            if (!PlayerGhostAwake.isInRangeOfGhost)
            {
                break;
            }
        }

        OnGhostSleep();

        PlayerGhostAwake.StartDecay();


        yield return new WaitForSeconds(coolDown);
        elapsedTime = coolDown;

        IsGhostAwake = false;

    }


}