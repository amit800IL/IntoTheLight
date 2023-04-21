using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour
{

    private bool IsGhostAwake = false;
    [field: SerializeField] public Light Light { get; private set; }

    private void Start()
    {
        CheckIfGhostAwake();
    }
    private void Update()
    {
        AwakeGhost();
        GameManager.Instance.Death(IsGhostAwake);
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

    public IEnumerator GhostFromWakeToSleep()
    {
        OnPlayerAwakeGhost();
        yield return new WaitForSeconds(3);
        OnGhostGoToSleep();
    }



}


