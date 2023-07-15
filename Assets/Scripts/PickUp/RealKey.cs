using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RealKey : MonoBehaviour, IInput
{
    [HideInInspector] public bool Haskey { get; private set; } = false;

    [SerializeField] private InputActionsSO InputActions;

    [SerializeField] private Transform chestLock;

    private bool pickUpAllowed = false;

    private bool isInChestTrigger;


    private void OnEnable()
    {
        InputActions.Interaction.performed += OnInteraction;
        InputActions.Interaction.canceled += OnInteraction;
    }

    private void OnDisable()
    {
        InputActions.Interaction.performed -= OnInteraction;
        InputActions.Interaction.canceled -= OnInteraction;
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && pickUpAllowed && isInChestTrigger)
        {
            StartCoroutine(OpenChestAndPickUp());
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
            isInChestTrigger = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
            isInChestTrigger = false;
        }
    }

    private IEnumerator OpenChestAndPickUp()
    {
        if (isInChestTrigger)
        {
            chestLock.Rotate(-90, 0, 0);
        }
        yield return new WaitForSeconds(3);

        gameObject.SetActive(false);
        Haskey = true;
        Debug.Log(Haskey);
    }

}
