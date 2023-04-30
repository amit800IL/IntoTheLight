using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour
{

    [field: SerializeField] public Light Light { get; private set; }

    [SerializeField] private GameObject GhostLight;

    private bool IsGhostAwake = false;

    private void Start()
    {
        GhostLight.SetActive(false);
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
            Light.spotAngle = default;
            Light.intensity = default;
        }
    }

    public void AwakeGhost()
    {
        bool keyPress = Keyboard.current.fKey.isPressed;

        if (keyPress && !IsGhostAwake && Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) < 2f)
        {
            IsGhostAwake = true;
            GhostLight.SetActive(true);
            StartCoroutine(GhostFromWakeToSleep());
        }
    }



    public void OnPlayerAwakeGhost()
    {
        Light.spotAngle = 200f;
        Light.intensity = 140;

    }

    public void OnGhostGoToSleep()
    {
        Light.spotAngle = default;
        Light.intensity = default;
        IsGhostAwake = false;
        GhostLight.SetActive(false);
    }

    public IEnumerator GhostFromWakeToSleep()
    {
        OnPlayerAwakeGhost();
        yield return new WaitForSeconds(3);
        OnGhostGoToSleep();
    }



}


