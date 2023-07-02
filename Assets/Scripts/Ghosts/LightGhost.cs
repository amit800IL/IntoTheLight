using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightGhost : MonoBehaviour, IInteractor
{
    [SerializeField] private Light Light;
    [SerializeField] public ParticleSystem GhostHealingLight;
    [SerializeField] private EnemyListSO enemyStats;
    [SerializeField] private PlayerEvents PlayerEvents;
    [SerializeField] private GhostEvents ghostEvents;
    [SerializeField] private PlayerVoiceSO playerVoiceScriptable;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private CoolDownSO coolDownDuration;
    [SerializeField] private PlayerStatsSO playerStats;

    private bool isPlayerInRange = false;

<<<<<<< HEAD
    private bool isInteracting = false;

    [field: SerializeField] public bool IsGhostAwake { get; private set; } = false;

    private Coroutine WakeCourtine;

    [SerializeField] private bool isAwake = false;
=======
    public bool IsGhostAwake = false;

    private Coroutine WakeCourtine;

    public EnemyListSO enemyList;
>>>>>>> Fixes

    private void Start()
    {
        GameManager.Instance.GhostManager.Ghosts.Add(this);
        Debug.Log("LightGhost Start");
        InputActions.Enable();
        CheckIfGhostAwake();
        ghostEvents.OnGhostAwake += OnGhostAwake;
        ghostEvents.OnGhostSleep += OnGhostSleep;
        InputActions.Interaction.performed += OnInteraction;
    }

    private void OnDestroy()
    {
        ghostEvents.OnGhostAwake -= OnGhostAwake;
        ghostEvents.OnGhostSleep -= OnGhostSleep;
        InputActions.Interaction.performed -= OnInteraction;
        InputActions.Disable();
    }

    private void CheckIfGhostAwake()
    {
        if (!isAwake)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

<<<<<<< HEAD
    public void OnInteraction(InputAction.CallbackContext context)
=======
    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyList.enemyList.Count);
        GameObject selectedEnemy = enemyList.enemyList[randomIndex];
        Instantiate(selectedEnemy, transform.position + new Vector3(0, 0, 2), transform.rotation);
        selectedEnemy.SetActive(true);
    }

    public void AwakeGhost()
>>>>>>> Fixes
    {
        if (!isInteracting && !isAwake && GameManager.Instance.GhostManager.activeGhost == null)
        {
<<<<<<< HEAD
            isInteracting = true;
            WakeCourtine = StartCoroutine(WakeUpGhost());
        }
    }

    public void SpawnEnemy()
=======
            IsGhostAwake = true;
            SpawnEnemy();
            WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
        }
    }

    public void OnGhostAwake()
>>>>>>> Fixes
    {
        int randomIndex = Random.Range(0, enemyStats.enemyList.Count);
        GameObject selectedEnemy = enemyStats.enemyList[randomIndex];
        Instantiate(selectedEnemy, transform.position + new Vector3(0, 0, 2), transform.rotation);
        selectedEnemy.SetActive(true);
    }

<<<<<<< HEAD
    public void StartHealing()
    {
        if (isAwake && WakeCourtine == null)
        {
            WakeCourtine = StartCoroutine(HealPlayer());
        }
    }

    public void StopHealing()
=======
    public void OnGhostSleep()
>>>>>>> Fixes
    {
        if (WakeCourtine != null)
        {
            StopCoroutine(WakeCourtine);
            WakeCourtine = null;
        }
    }

    private IEnumerator WakeUpGhost()
    {
<<<<<<< HEAD
        isAwake = true;
        GameManager.Instance.GhostManager.activeGhost = this;

        GhostHealingLight.Play();
        Light.spotAngle = 60f;
        Light.intensity = 60f;

        ghostEvents.InvokeGhostAwake();
        PlayerEvents.InvokePlayerHeal();

        SpawnEnemy();

        yield return new WaitForSeconds(coolDownDuration.CoolDownDuration);

        isAwake = false;
        GameManager.Instance.GhostManager.activeGhost = null;

        GhostHealingLight.Stop();
        GhostHealingLight.Clear();
        Light.spotAngle = 30f;
        Light.intensity = 30f;

        isInteracting = false;
=======
        OnGhostAwake();
        yield return new WaitForSeconds(7);
        OnGhostSleep();
>>>>>>> Fixes
    }

    private IEnumerator HealPlayer()
    {
        while (isAwake)
        {
            playerStats.HP += 3f;
            yield return new WaitForSeconds(coolDownDuration.CoolDownDuration);

            if (!isPlayerInRange)
            {
                break;
            }
        }
    }

    public void OnGhostAwake()
    {
        GhostHealingLight.Play();
        Light.spotAngle = 60f;
        Light.intensity = 60f;
    }

    public void OnGhostSleep()
    {
        GhostHealingLight.Stop();
        GhostHealingLight.Clear();
        Light.spotAngle = 30f;
        Light.intensity = 30f;
    }

}