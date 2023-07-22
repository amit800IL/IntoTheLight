using UnityEngine;

public class InRoomBehavior : MonoBehaviour
{
    public bool isPlayerInsideRoom { get; private set; } = false;
    [field: SerializeField] public AudioSource hitDoor { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInsideRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInsideRoom = false;
        }
    }
}
