using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FakeKey : MonoBehaviour, ICheckPlayerInteraction
{
    [HideInInspector] public bool Haskey { get; private set; } = false;

    [SerializeField] private InputActionsSO InputActions;

    [SerializeField] private Transform chestLock;

    [SerializeField] private Enemy enemy;

    private bool pickUpAllowed = false;

    private bool isInChestTrigger;


    private void OnEnable()
    {
        InputActions.Interaction.performed += OnPlayerInteraction;
        InputActions.Interaction.canceled += OnPlayerInteraction;
    }

    private void OnDisable()
    {
        InputActions.Interaction.performed -= OnPlayerInteraction;
        InputActions.Interaction.canceled -= OnPlayerInteraction;
    }

    public void OnPlayerInteraction(InputAction.CallbackContext context)
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

        yield return new WaitForSeconds(5);

        enemy.agent.Warp(GameManager.Instance.Player.transform.position);

        enemy.enemyKill();
    }

}
