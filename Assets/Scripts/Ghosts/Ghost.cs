using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{

    [SerializeField] private Light Light;
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
            Light.spotAngle = default;
            Light.intensity = default;
        }
    }

    public void AwakeGhost()
    {
        bool keyPress = Keyboard.current.fKey.isPressed;

        if (keyPress && !IsGhostAwake)
        {
            IsGhostAwake = true;
            StartCoroutine(GhostFromWakeToSleep());
        }
    }


    public void OnPlayerAwakeGhost()
    {
        Light.spotAngle = 100f;
        Light.intensity = 70;
    }

    public void OnGhostGoToSleep()
    {
        Light.spotAngle = default;
        Light.intensity = default;
        IsGhostAwake = false;
    }

    IEnumerator GhostFromWakeToSleep()
    {
        OnPlayerAwakeGhost();
        yield return new WaitForSeconds(3);
        OnGhostGoToSleep();
    }



}


