using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGhostMediator : MonoBehaviour /*IInput*/
{
    //private InputAction.CallbackContext input;

    //[SerializeField] private LightGhost[] ghost;

    //[SerializeField] private InputActionsSO InputActions;

    //private void Start()
    //{
    //    ghost = FindObjectsOfType<LightGhost>();
    //    Debug.LogError(ghost.Length);
    //}

    //private void OnEnable()
    //{
    //    InputActions.Interaction.performed += OnInteraction;
    //    InputActions.Interaction.canceled += OnInteraction;
    //}

    //private void OnDisable()
    //{
    //    InputActions.Interaction.performed -= OnInteraction;
    //    InputActions.Interaction.canceled -= OnInteraction;
    //}

    //public void OnInteraction(InputAction.CallbackContext context)
    //{
    //    input = context;

    //    GameManager.Instance.PlayerGhostAwake.OnInteraction(input);

    //    foreach (LightGhost ghost in ghost)
    //    {
    //        ghost.OnInteraction(input);
    //    }
    //}



    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("GhostLight"))
    //    {
    //        Debug.LogError("Interact collision detected");
    //        input = new();
    //        OnInteraction(input);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("GhostLight"))
    //    {
    //        foreach (LightGhost ghost in ghost)
    //        {
    //            if (!GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost && ghost.IsGhostAwake)
    //            {
    //                ghost.OnGhostSleep();
    //            }
    //        }
    //    }
    //}
}