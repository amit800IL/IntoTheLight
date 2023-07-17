using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwake : MonoBehaviour
{

    [field : Header("General")]
    [field : SerializeField] public ParticleSystem playerHealingEffect { get; private set; }
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get => hasAwaknedGhost; set => hasAwaknedGhost = value; }

    private bool hasAwaknedGhost;

    private LightGhost ghost;

    [Header("Health Up and Down")]

    [SerializeField] public float SanityUpNumber;
    [SerializeField] public float SanityDownNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            ghost = other.GetComponentInParent<LightGhost>();
            isInRangeOfGhost = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GhostLight"))
        {
            ghost = null;
            isInRangeOfGhost = false;
        }
    }

}