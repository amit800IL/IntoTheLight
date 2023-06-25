using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour
{
    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private ParticleSystem GhostHealingLight;

    private bool IsGhostAwake = false;

    private Coroutine WakeCourtine;

    private void Start()
    {
        CheckIfGhostAwake();
    }
    private void Update()
    {
        AwakeGhost();
    }
    private void CheckIfGhostAwake()
    {
        if (!IsGhostAwake && !GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void AwakeGhost()
    {
        bool keyPress = Keyboard.current.eKey.isPressed;

        if (keyPress && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f && !GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            IsGhostAwake = true;
            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }

    public void OnPlayerAwakeGhost()
    {
        GhostHealingLight.Play();
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
        OnPlayerAwakeGhost();
        yield return new WaitForSeconds(7);
        OnGhostGoToSleep();
    }
}


