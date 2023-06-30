using UnityEngine;

public class VoiceManager : MonoBehaviour
{

    [Header("Player")]
    public PlayerVoiceSO playerVoiceScriptable;

    [SerializeField] private AudioSource playerOhNoScream;
    [SerializeField] private AudioSource guardGettingCloser;
    [SerializeField] private AudioSource playerBreathing;
    [SerializeField] private AudioSource playerScream;
    [SerializeField] private AudioSource secondPlayerScream;
    [SerializeField] private AudioSource playerWalk;

    private void Start()
    {
        playerVoiceScriptable.playerOhNoScream = playerOhNoScream;
        playerVoiceScriptable.guardGettingCloser = guardGettingCloser;
        playerVoiceScriptable.playerBreathing = playerBreathing;
        playerVoiceScriptable.playerScream = playerScream;
        playerVoiceScriptable.secondPlayerScream = secondPlayerScream;
        playerVoiceScriptable.playerWalk = playerWalk;
    }
}
