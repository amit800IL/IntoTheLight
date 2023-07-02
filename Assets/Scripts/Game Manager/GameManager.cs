using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GhostManager GhostManager;

    [field: Header("Player Scripts Refernces")]
    [field: SerializeField] public Transform Player { get; private set; }

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
