using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour
{
    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private ParticleSystem GhostHealingLight;

    private bool IsGhostAwake = false;

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
        if (!IsGhostAwake)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void AwakeGhost()
    {
        bool keyPress = Keyboard.current.fKey.isPressed;

        if (keyPress && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f)
        {
            IsGhostAwake = true;
            StartCoroutine(GhostFromWakeToSleep());
        }
    }



    public void OnPlayerAwakeGhost()
    {
        GhostHealingLight.Play();
    }

    public void OnGhostGoToSleep()
    {
        GhostHealingLight.Stop();
        Light.spotAngle = 40f;
        Light.intensity = 40f;
        IsGhostAwake = false;
    }

    public IEnumerator GhostFromWakeToSleep()
    {
        OnPlayerAwakeGhost();
        yield return new WaitForSeconds(10);
        OnGhostGoToSleep();
    }



}


