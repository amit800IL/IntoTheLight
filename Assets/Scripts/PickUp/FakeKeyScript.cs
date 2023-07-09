using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FakeKeyScript : MonoBehaviour , IInput
{
    private bool pickUpAllowed = false;

    [SerializeField] private Enemy guard;

    [SerializeField] private InputActionsSO InputActions;

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

            Vector3 offset = Random.onUnitSphere * guard.EnemySpeed;

            offset.y = 0;

            guard.agent.Warp(GameManager.Instance.Player.transform.position);
        }
    }

    private void PickUp()
    {
        gameObject.SetActive(false);
    }
  
}
