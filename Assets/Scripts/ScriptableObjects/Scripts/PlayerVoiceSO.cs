using UnityEngine;


[CreateAssetMenu(fileName = "PlayerVoice", menuName = "ScriptableObjects/PlayerVoice")]
public class PlayerVoiceSO : ScriptableObject
{
    [Header("Player AI Voice Acting")]
    [HideInInspector] public AudioSource playerOhNoScream;
    [HideInInspector] public AudioSource guardGettingCloser;

    [Header("Player Voices")]
    [HideInInspector] public AudioSource playerBreathing;
    [HideInInspector] public AudioSource playerScream;
    [HideInInspector] public AudioSource secondPlayerScream;
    [HideInInspector] public AudioSource playerWalk;
}

