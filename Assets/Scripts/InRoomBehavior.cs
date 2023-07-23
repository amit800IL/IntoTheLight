using System.Collections;
using UnityEngine;

public class InRoomBehavior : MonoBehaviour
{
    public bool isPlayerInsideRoom { get; private set; } = false;
    [field: SerializeField] public AudioSource hitDoor { get; private set; }

    [SerializeField] private Transform mainCamera;

    //private IEnumerator shakeCamera()
    //{
    //    Vector3 originalPos = mainCamera.position;

    //    float x = Random.Range(-1f,1f) * originalPos.magnitude;
    //}
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
