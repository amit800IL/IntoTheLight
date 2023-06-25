using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: SerializeField] public Guard guard { get; private set; }

    [field: Header("Player Scripts Refernces")]
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public PlayerGhostAwake PlayerGhostAwake { get; private set; }
    [field: SerializeField] public PlayerVoice PlayerVoice { get; private set; }
    [field: SerializeField] public PlayerOpenDoor openDoor { get; private set; }

    [field: Header("Colliders Refernces")]
    [field: SerializeField] public Collider[] safeRoomDoor { get; private set; }

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }

}
