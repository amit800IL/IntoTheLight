using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    [field: Header("General")]
    [field: SerializeField] public NavMeshAgent agent { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }

    [SerializeField] private SkinnedMeshRenderer enemyRenderer;
    [SerializeField] private Light enemyLight;
    [SerializeField] private AudioSource[] enemyScreams;
    [SerializeField] private InputActionsSO inputActions;
    private bool isChasingPlayer;
    private bool canKillPlayer = true;
    private Vector3 PlayerPosition;

    [field: Header("Numbers")]
    public float EnemySpeed { get; private set; }
    private float maxDelay = 2f;
    private float minDelay = 5f;
    private float minEnemySpeed = 3f;
    private float maxEnemySpeed = 6f;
    private float minkillingDistance = 3f;
    private float maxKillingDistance = 10f;
    private float killingDistance;
    private float distance;

    [field: Header("Audio Sources")]
    [field: SerializeField] public AudioSource guardWalkSound { get; private set; }
    [SerializeField] private AudioSource guardScream;
    [SerializeField] private AudioSource guardKillingScream;


    private void Start()
    {
        isChasingPlayer = false;
        killingDistance = Random.Range(minkillingDistance, maxKillingDistance);
        EnemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
        agent.speed = EnemySpeed;
        StartCoroutine(ChasePlayer());
    }

    private void Update()
    {
        PlayerPosition = GameManager.Instance.Player.transform.position;
    }
    private void ScarePlayer()
    {
        float WalkAwayDistanceFraction = 0.5f;
        float walkAwayDistance = Vector3.Distance(PlayerPosition, transform.position);
        float targetDistance = walkAwayDistance * WalkAwayDistanceFraction;

        Vector3 WalkDirection = PlayerPosition - transform.position;
        WalkDirection.y = 0f;
        Vector3 spawnPosition = transform.position - WalkDirection.normalized * targetDistance;
        agent.SetDestination(spawnPosition);
        Debug.Log("Scare Player" + spawnPosition);
    }

    private void StandInFrontOfPlayer()
    {
        float offset = 10f;

        Vector3 directionToPlayer = transform.position - PlayerPosition;
        Vector3 targetPosition = PlayerPosition + directionToPlayer.normalized * offset;
        agent.Warp(targetPosition);
        Debug.Log("stand in front of player" + targetPosition);
    }
    private void GoToPlayer()
    {
        float offset = killingDistance + 2f;

        Vector3 directionToPlayer = transform.position - PlayerPosition;
        directionToPlayer.y = 0f;
        Vector3 targetPosition = PlayerPosition + directionToPlayer.normalized * offset;

        animator.SetTrigger("IsWalking");
        agent.SetDestination(targetPosition);
        Debug.Log("go to player : " + targetPosition);
    }
    private IEnumerator ChasePlayer()
    {

        enemyRenderer.forceRenderingOff = true;

        isChasingPlayer = true;

        enemyLight.enabled = false;

        yield return new WaitForSeconds(5);

        enemyRenderer.forceRenderingOff = false;

        enemyLight.enabled = true;

        for (int i = 0; i < 2; i++)
        {

            animator.SetTrigger("IsWalking");
            ScarePlayer();

            guardWalkSound.Play();

            AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            enemyLight.enabled = false;

            animator.SetTrigger("IsWalking");
            ScarePlayer();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            animator.SetTrigger("IsWalking");
            ScarePlayer();

            enemyRenderer.forceRenderingOff = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            animator.SetTrigger("IsWalking");
            ScarePlayer();

            guardWalkSound.Stop();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            enemyRenderer.forceRenderingOff = false;

            enemyLight.enabled = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            guardWalkSound.Play();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            animator.SetTrigger("IsWalking");
            ScarePlayer();

            enemyRenderer.forceRenderingOff = true;

            enemyLight.enabled = false;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            yield return new WaitForSeconds(2);

            randomScream.Stop();

        }

        canKillPlayer = true;
        isChasingPlayer = true;
        enemyLight.enabled = true;
        enemyRenderer.forceRenderingOff = false;

        while (isChasingPlayer)
        {
            distance = Vector3.Distance(agent.transform.position, PlayerPosition);

            guardWalkSound.Play();

            GoToPlayer();

            standInFronOfGhost();

            if (agent != null && distance < killingDistance && canKillPlayer)
            {
                enemyKill();

                yield return new WaitForSeconds(2);

                if (GameManager.Instance.playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

            }
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
    }

    public void enemyKill()
    {
        inputActions.Move.Disable();
        inputActions.Look.Disable();
        animator.ResetTrigger("IsWalking");
        animator.SetTrigger("IsAttacking");

        Vector3 enemyPosition = transform.position;

        GameManager.Instance.Player.transform.LookAt(enemyPosition);
        Camera.main.transform.LookAt(enemyPosition);

        Quaternion playerRotation = Quaternion.Euler(-90, GameManager.Instance.Player.transform.rotation.eulerAngles.y, 0);
        GameManager.Instance.Player.transform.rotation = playerRotation;

        agent.isStopped = true;
        guardKillingScream.Play();
        guardKillingScream.volume = 1f;
        GameManager.Instance.playerStats.HP -= 200;
        PlayerVoiceManager.Instance.playerScream.Play();
        PlayerVoiceManager.Instance.secondPlayerScream.Play();

        StartCoroutine(standByPlayer());
    }

    private IEnumerator standByPlayer()
    {
        while (true)
        {
            transform.position = PlayerPosition;

            yield return null;
        }
    }
    private void standInFronOfGhost()
    {
        if (GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost && GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            Debug.Log("StandingInFront");
            animator.ResetTrigger("IsWalking");
            animator.SetTrigger("IsStanding");
            StandInFrontOfPlayer();
            agent.isStopped = true;
            canKillPlayer = false;
        }
        else if (!GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost && !GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
        {
            Debug.Log("NotStandingInFront");
            animator.ResetTrigger("IsStanding");
            animator.SetTrigger("IsWalking");
            agent.isStopped = false;
            canKillPlayer = true;
        }
    }

}
