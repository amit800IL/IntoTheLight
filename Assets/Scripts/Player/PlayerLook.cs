using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector3 newLook;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionsSO InputActions;

    private void OnEnable()
    {
        InputActions.Look.performed += OnLook;
        InputActions.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        InputActions.Look.performed -= OnLook;
        InputActions.Look.canceled -= OnLook;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        newLook = context.ReadValue<Vector2>();

        float LookSpeed = GameManager.Instance.playerStats.lookSpeed;

        float Xrotation = newLook.y * LookSpeed * Time.deltaTime;
        Xrotation = Mathf.Clamp(Xrotation, -90, 90);

        cameraTransform.Rotate(-Xrotation, 0, 0);
        playerTransform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);
    }

}