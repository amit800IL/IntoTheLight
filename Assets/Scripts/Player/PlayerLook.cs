using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector3 newLook;
    [SerializeField] private Transform orientation;
    [SerializeField] private InputActionsSO InputActions;
<<<<<<< HEAD
    [SerializeField] private PlayerStatsSO playerStats;
=======
>>>>>>> Fixes

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Look.performed -= OnLook;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        newLook = context.ReadValue<Vector2>();

<<<<<<< HEAD
        transform.Rotate(0, newLook.x * playerStats.lookSpeed * Time.deltaTime, 0);
=======
        transform.Rotate(0, newLook.x * GameManager.Instance.playerStats.lookSpeed, 0);
>>>>>>> Fixes
    }

}