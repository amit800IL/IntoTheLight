using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private IntoTheLight inputActions;
    public IntoTheLight InputActions { get => inputActions; private set => inputActions = value; }
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
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        Instance = this;
        InputActions = new();
        InputActions.Enable();

        InputActions.Player.GhostWake.started += OnPressStart;

        Debug.Log("Input start called");
    }

    public void OnPressStart(InputAction.CallbackContext context)
    {
        OnGhostAwakening?.Invoke();
        Debug.Log("Invoked");
    }

    public Vector2 GetMoveValue(InputValue input)
    {
        return input.Get<Vector2>();
    }

    public Vector2 GetButtonPressValue(InputValue input)
    {
        return input.Get<Vector2>();
    }


}
