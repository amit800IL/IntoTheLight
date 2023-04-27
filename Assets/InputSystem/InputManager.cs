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
        Instance = this;
        InputActions = new();
        InputActions.Enable();

    }

    public Vector2 GetMoveValue(InputValue input) => input.Get<Vector2>();
    public Vector2 GetLookValue(InputValue input) => input.Get<Vector2>();

}

        


