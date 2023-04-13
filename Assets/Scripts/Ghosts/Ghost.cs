using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{

    [SerializeField] private Light Light;
    [SerializeField] InputValue Input;

    private void Update()
    {
        OnPlayerAwakeGhost();
    }

    public void OnPlayerAwakeGhost()
    {
        if (GameManager.Instance != null && GameManager.Instance.PlayerGhostAwake != null && Vector3.Distance(GameManager.Instance.PlayerGhostAwake.transform.position, transform.position) < 0.5f)
        {
            CheckPressValue();
            Light.spotAngle = 100f;
            Light.intensity = 70;
            Debug.Log("HELP");

        }
        else
        {
            Light.spotAngle = default;
            Light.intensity = default;
        }

    }

    private bool CheckPressValue()
    {
        if (Input.isPressed)
        {
            InputManager.Instance.GetButtonPressValue(Input);
            Debug.Log("noooooooo");
            return true;
        }
        return false;
    }

    




}
