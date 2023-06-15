using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField] public GameObject Ghost { get; private set; }

    [field: Header("Player Scripts Refernces")]
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }

    [field: Header("Audio Sources Refernces")]
    [field: SerializeField] public AudioSource playerScream { get; private set; }
    [field: SerializeField] public AudioSource playerBreathing { get; private set; }
    [field: SerializeField] public AudioSource secondPlayerScream { get; private set; }

    [field : Header("Colliders Refernces")]
    [field: SerializeField] public Collider playerCollider { get; private set; }
    [field: SerializeField] public Collider[] ghostCollider { get; private set; }
    [field: SerializeField] public Collider[] safeRoomDoor { get; private set; }
    [field: SerializeField] public Collider GuardCollider { get; private set; }



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
