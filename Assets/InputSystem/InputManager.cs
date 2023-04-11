using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private IntoTheLight inputActions;
    public UnityEvent OnGhostAwakening { get; set; }
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        inputActions = new();
        inputActions.Enable();

        inputActions.Player.GhostWake.started += OnPressStart;
        
    }

    public void OnPressStart(InputAction.CallbackContext context)
    {
        OnGhostAwakening?.Invoke();
    }

    public Vector2 GetMoveValue(InputValue input)
    {
        return input.Get<Vector2>();
    }

    public Vector2 GetButtonPressValue()
    {
        return inputActions.Player.GhostWake.ReadValue<Vector2>();
    }


}
