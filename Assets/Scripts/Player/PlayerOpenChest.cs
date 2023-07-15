using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenChest : MonoBehaviour, IInput
{
    [SerializeField] private Transform[] chestLock;
    [SerializeField] private InputActionsSO inputActions;
    private bool isInChestTrigger;

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
        if (context.performed && isInChestTrigger)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        foreach (Transform checlock in chestLock)
        {
            if (isInChestTrigger)
            {
                checlock.Rotate(-90, 0, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SafeRoom"))
        {
            isInChestTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeRoom"))
        {
            isInChestTrigger = false;

            foreach (Transform RightDoor in chestLock)
            {
                if (!isInChestTrigger)
                {
                    RightDoor.rotation = default;
                }

            }
        }
    }
}
