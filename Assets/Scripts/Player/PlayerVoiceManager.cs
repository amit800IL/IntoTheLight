using UnityEngine;

public class PlayerVoiceManager : MonoBehaviour
{
    public static PlayerVoiceManager Instance { get; private set; }

    [field: Header("Player AI Voice Acting")]
    [field: SerializeField] public AudioSource PlayerOhNoScream { get; private set; }
    [field: SerializeField] public AudioSource GuardGettingCloser { get; private set; }

    [field: Header("Player Voices")]
    [field: SerializeField] public AudioSource playerBreathing { get; private set; }
    [field: SerializeField] public AudioSource playerScream { get; private set; }
    [field: SerializeField] public AudioSource secondPlayerScream { get; private set; }
    [field: SerializeField] public AudioSource playerWalk { get; private set; }



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
