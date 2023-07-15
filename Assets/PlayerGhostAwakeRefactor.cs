using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerGhostAwakeRefactor : MonoBehaviour
{
    public bool isInRangeOfGhost { get; private set; } = false;
    public bool HasAwaknedGhost { get; private set; } = false;

    [Header("General")]

    private LightGhost ghost;
    private bool shouldHeal;
    [SerializeField] private CoolDownSO coolDown;
    [SerializeField] private ParticleSystem playerHealingEffect;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private GhostEvents ghostEvents;

    [Header("Coroutines")]

    private Coroutine healingCourtuine;
    private Coroutine decayCourtuine;

    [Header("Health Up and Down")]

    [SerializeField] private float SanityUpNumber;
    [SerializeField] private float SanityDownNumber;

    private InputAction.CallbackContext context;

  

}
