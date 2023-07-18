using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenKeyRoomDoor : MonoBehaviour, ICheckPlayerInteraction
{
    private bool isInDoorTrigger = false;
    private bool isDoorOpen = false;
    private Vector3[] rightDoorDefaultPositions;
    private Quaternion[] rightDoorDefaultRotations;
    private Vector3[] leftDoorDefaultPositions;
    private Quaternion[] leftDoorDefaultRotations;
    [SerializeField] private Transform[] rightDoor;
    [SerializeField] private Transform[] leftDoor;
    [SerializeField] private InputActionsSO inputActions;

    private void Start()
    {
        StoreDoorsDefault();
    }

    private void OnEnable()
    {
        inputActions.Interaction.performed += OnPlayerInteraction;
        inputActions.Interaction.canceled += OnPlayerInteraction;
    }

    private void OnDisable()
    {
        inputActions.Interaction.performed -= OnPlayerInteraction;
        inputActions.Interaction.canceled -= OnPlayerInteraction;
    }

    public void OnPlayerInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && isInDoorTrigger)
        {
            if (!isDoorOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    private void OpenDoor()
    {
        isDoorOpen = true;

        foreach (Transform RightDoor in rightDoor)
        {
            if (isInDoorTrigger)
            {
                RightDoor.Rotate(0, -90, 0);
                RightDoor.position = new Vector3(0, -0.3f, 0);
            }
        }

        foreach (Transform LeftDoor in leftDoor)
        {
            if (isInDoorTrigger)
            {
                LeftDoor.Rotate(0, 90, 0);
                LeftDoor.position = new Vector3(0, 0.3f, 0);
            }
        }
    }

    private IEnumerator CloseDoorWithDelay()
    {
        yield return new WaitForSeconds(1f);

        if (!isInDoorTrigger)
        {
            CloseDoor();
        }
    }

    private void CloseDoor()
    {
        isDoorOpen = false;

        for (int i = 0; i < rightDoor.Length; i++)
        {
            if (!isInDoorTrigger)
            {
                rightDoor[i].position = rightDoorDefaultPositions[i];
                rightDoor[i].rotation = rightDoorDefaultRotations[i];
            }
        }

        for (int i = 0; i < leftDoor.Length; i++)
        {
            if (!isInDoorTrigger)
            {
                leftDoor[i].position = leftDoorDefaultPositions[i];
                leftDoor[i].rotation = leftDoorDefaultRotations[i];
            }
        }


    }

    private void StoreDoorsDefault()
    {
        rightDoorDefaultPositions = new Vector3[rightDoor.Length];
        rightDoorDefaultRotations = new Quaternion[rightDoor.Length];

        for (int i = 0; i < rightDoor.Length; i++)
        {
            rightDoorDefaultPositions[i] = rightDoor[i].position;
            rightDoorDefaultRotations[i] = rightDoor[i].rotation;
        }

        leftDoorDefaultPositions = new Vector3[leftDoor.Length];
        leftDoorDefaultRotations = new Quaternion[leftDoor.Length];

        for (int i = 0; i < leftDoor.Length; i++)
        {
            leftDoorDefaultPositions[i] = leftDoor[i].position;
            leftDoorDefaultRotations[i] = leftDoor[i].rotation;
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
            StartCoroutine(CloseDoorWithDelay());
        }
    }
}