using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenKeyRoomDoor : MonoBehaviour, IInput
{
    [SerializeField] private Transform[] rightDoor;
    [SerializeField] private Transform[] leftDoor;
    [SerializeField] private InputActionsSO inputActions;
    private bool isInDoorTrigger;

    private void OnEnable()
    {
        inputActions.Interaction.performed += OnInteraction;
        inputActions.Interaction.canceled += OnInteraction;
    }

    private void OnDisable()
    {
        inputActions.Interaction.performed -= OnInteraction;
        inputActions.Interaction.canceled -= OnInteraction;
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && isInDoorTrigger)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        foreach (Transform RightDoor in rightDoor)
        {
            if (isInDoorTrigger)
            {
                RightDoor.Rotate(0, -90, 0);
            }
        }
        foreach (Transform LeftDoor in leftDoor)
        {
            if (isInDoorTrigger)
            {
                LeftDoor.Rotate(0, 90, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeRoom"))
        {
            isInDoorTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeRoom"))
        {
            isInDoorTrigger = false;

            foreach (Transform RightDoor in rightDoor)
            {
                if (!isInDoorTrigger)
                {
                    RightDoor.rotation = default;
                }

            }
            foreach (Transform LeftDoor in leftDoor)
            {
                if (!isInDoorTrigger)
                {
                    LeftDoor.rotation = default;
                }

            }
        }
    }
}
