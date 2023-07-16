using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerGhostMediator : MonoBehaviour
{
    [SerializeField] private LightGhost[] ghost;

    private InputAction.CallbackContext input;

    private void Start()
    {
        ghost = FindObjectsOfType<LightGhost>();
    }

    public void OnPlayerInteract()
    {
        foreach (LightGhost ghost in ghost)
        {
            ghost.OnInteraction(input);
            GameManager.Instance.PlayerGhostAwake.OnInteraction(input);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            OnPlayerInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            foreach (LightGhost ghost in ghost)
            {
                if (!GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost && ghost.IsGhostAwake)
                {
                    ghost.OnGhostSleep();
                }
            }
        }
    }
}