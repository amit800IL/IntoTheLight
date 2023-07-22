using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [field: Header("General")]
    [field: SerializeField] public NavMeshAgent agent { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }

    [SerializeField] private SkinnedMeshRenderer enemyRenderer;
    [SerializeField] private Light enemyLight;
    [SerializeField] private AudioSource[] enemyScreams;
    [SerializeField] private InputActionsSO inputActions;
    [SerializeField] private InRoomBehavior[] rooms;
    private bool isChasingPlayer;
    private bool canKillPlayer = true;
    private Vector3 PlayerPosition;

    [field: Header("Numbers")]
    public float EnemySpeed { get; private set; }
    private float maxDelay = 5f;
    private float minDelay = 7f;
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
        StartCoroutine(ChasePlayer());
    }

    private void Update()
    {
        PlayerPosition = GameManager.Instance.Player.transform.position;
    }
    private void ScarePlayer()
    {
        float WalkAwayDistanceFraction = 3f;

        Vector3 WalkDirection = transform.position - PlayerPosition;
        WalkDirection.y = 0f;
        Vector3 spawnPosition = PlayerPosition - WalkDirection.normalized * WalkAwayDistanceFraction;
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
        PlayerPosition = GameManager.Instance.Player.transform.position;
        isChasingPlayer = false;
        killingDistance = Random.Range(minkillingDistance, maxKillingDistance);
        EnemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
        agent.speed = EnemySpeed;

        animator.SetTrigger("IsWalking");
        ScarePlayer();

        enemyRenderer.forceRenderingOff = true;

        isChasingPlayer = true;

        enemyLight.enabled = false;


        yield return new WaitForSeconds(5);

        PlayerPosition = GameManager.Instance.Player.transform.position;

        ScarePlayer();

        enemyRenderer.forceRenderingOff = false;

        enemyLight.enabled = true;

        AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
        randomScream.Play();

        guardWalkSound.Play();

        randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
        randomScream.Play();

        enemyLight.enabled = false;
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

        PlayerPosition = GameManager.Instance.Player.transform.position;

        ScarePlayer();

        enemyRenderer.forceRenderingOff = true;

        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

        PlayerPosition = GameManager.Instance.Player.transform.position;

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

        yield return new WaitForSeconds(2);

        randomScream.Stop();

        canKillPlayer = true;
        isChasingPlayer = true;
        enemyLight.enabled = true;
        enemyRenderer.forceRenderingOff = false;

        while (isChasingPlayer)
        {
            distance = Vector3.Distance(agent.transform.position, PlayerPosition);

            guardWalkSound.Play();

            enemyAttackDoor();

            GoToPlayer();

            standInFronOfGhost();

            if (agent != null && distance < killingDistance && canKillPlayer)
            {
                enemyKill();
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

    public void enemyAttackDoor()
    {
        foreach (InRoomBehavior rooms in rooms)
        {
            float enemyTriggerDistance = Vector3.Distance(rooms.transform.position, transform.position);

            if (enemyTriggerDistance < 50 && rooms.isPlayerInsideRoom)
            {
                rooms.hitDoor.Play();
                guardWalkSound.Stop();
                agent.isStopped = true;
                canKillPlayer = false;
                animator.ResetTrigger("IsWalking");
                animator.SetTrigger("IsAttacking");
            }
            else if (!rooms.isPlayerInsideRoom)
            {
                rooms.hitDoor.Stop();
                guardWalkSound.Play();
                agent.isStopped = false;
                canKillPlayer = true;
                animator.ResetTrigger("IsAttacking");
                animator.SetTrigger("IsWalking");
            }

        }
    }

}
