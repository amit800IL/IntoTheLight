using UnityEngine;

public class PlayerVoice : MonoBehaviour
{

    [field : Header("Player AI Voice Acting")]
    [field: SerializeField] public AudioSource PlayerOhNoScream { get; private set; }
    [field: SerializeField] public AudioSource GuardGettingCloser { get; private set; }
    [field: SerializeField] public AudioSource closeToKeyRoom { get; private set; }
    [field: SerializeField] public AudioSource ghostRangeVoice { get; private set; }

    [field: Header("Player Voices")]
    [field: SerializeField] public AudioSource playerBreathing { get; private set; }
    [field: SerializeField] public AudioSource playerScream { get; private set; }
    [field: SerializeField] public AudioSource secondPlayerScream { get; private set; }


    private void Update()
    {
        GettingClosetToKeyRoom();
    }

    //public void GettingCloserToDoor()
    //{
    //    float Distance = Vector3.Distance(transform.position, GameManager.Instance.openDoor.Door.transform.position);

    //    if (Distance < 10)
    //    {
    //        Debug.Log("Im getting closer to the door");
    //    }
    //}

    public void GettingClosetToKeyRoom()
    {
        foreach (Collider safeRoomDoor in GameManager.Instance.safeRoomDoor)
        {
            float Distance = Vector3.Distance(transform.position, safeRoomDoor.transform.position);

            if (Distance < 10)
            {
                closeToKeyRoom.Play();
            }
        }
    }
}
