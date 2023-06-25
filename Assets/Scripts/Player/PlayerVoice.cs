using UnityEngine;

public class PlayerVoice : MonoBehaviour
{

    [field: Header("Player AI Voice Acting")]
    [field: SerializeField] public AudioSource PlayerOhNoScream { get; private set; }
    [field: SerializeField] public AudioSource GuardGettingCloser { get; private set; }

    [field: Header("Player Voices")]
    [field: SerializeField] public AudioSource playerBreathing { get; private set; }
    [field: SerializeField] public AudioSource playerScream { get; private set; }
    [field: SerializeField] public AudioSource secondPlayerScream { get; private set; }

}
