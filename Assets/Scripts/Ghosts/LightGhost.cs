using System.Collections;
using UnityEngine;

public class LightGhost : MonoBehaviour
{
    [field: Header("General")]
    public bool IsGhostAwake { get; private set; } = false;

    private PlayerGhostAwake PlayerGhostAwake;

    [SerializeField] private Light Light;

    [SerializeField] private Enemy enemy;

    [SerializeField] private ParticleSystem GhostHealingLight;

    [SerializeField] private InputActionsSO InputActions;

    [field: Header("Numbers")]
    public float coolDown { get; private set; } = 7f;
    public float elapsedTime { get; private set; }

    [Header("Coroutines")]

    private Coroutine WakeCourtine;


    private void Start()
    {
        elapsedTime = coolDown;
        PlayerGhostAwake = GameManager.Instance.PlayerGhostAwake;
        //CheckIfGhostAwake();
    }

    private void CheckIfGhostAwake()
    {
        if (!IsGhostAwake && !PlayerGhostAwake.HasAwaknedGhost)
        {
            Light.spotAngle = 40f;
            Light.intensity = 40f;
        }
    }

    public void OnGhostInteraction()
    {
        OnGhostAwake();

        if (WakeCourtine != null)
        {
            StopCoroutine(WakeCourtine);
            WakeCourtine = null;
        }

        WakeCourtine = StartCoroutine(GhostFromWakeToSleep());
    }

    public void OnGhostAwake()
    {

        IsGhostAwake = true;

        PlayerVoiceManager.Instance.GuardGettingCloser.Stop();
        PlayerVoiceManager.Instance.PlayerOhNoScream.Stop();

        GhostHealingLight.Play();
    }

    public void OnGhostSleep()
    {
        GhostHealingLight.Stop();
        //Light.spotAngle = 40f;
        //Light.intensity = 40f;
    }

    private IEnumerator GhostFromWakeToSleep()
    {
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            yield return null;

            float enemyGhostDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemyGhostDistance < 10f && PlayerGhostAwake.isInRangeOfGhost)
            {
                transform.LookAt(enemy.transform.position);
            }

            if (!PlayerGhostAwake.isInRangeOfGhost)
            {
                break;
            }
        }

        OnGhostSleep();

        PlayerGhostAwake.StartDecay();


        yield return new WaitForSeconds(coolDown);
        elapsedTime = coolDown;

        IsGhostAwake = false;

    }


}