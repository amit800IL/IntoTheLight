using UnityEngine;
using UnityEngine.InputSystem;

public class KeyScript : MonoBehaviour, IInput
{
    [HideInInspector] public bool Haskey { get; private set; } = false;

    [SerializeField] private InputActionsSO InputActions;

    private bool pickUpAllowed = false;


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
        if (context.performed && pickUpAllowed)
        {
            PickUp();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        gameObject.SetActive(false);
        Haskey = true;
    }

}
