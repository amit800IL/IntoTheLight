using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{

    [Header("General")]

    protected bool isChasingPlayer;
    protected float distance;
    [SerializeField] protected bool canKillPlayer = true;
    [field: SerializeField] public NavMeshAgent agent { get; protected set; }
    [SerializeField] protected Light enemyLight;
    [SerializeField] protected Animator animator;

    [field: Header("Numbers")]

    protected float maxDelay = 2f;
    protected float minDelay = 5f;
    protected float minEnemySpeed = 3f;
    protected float maxEnemySpeed = 6f;
    protected float minkillingDistance = 3f;
    protected float maxKillingDistance = 10f;
    public float EnemySpeed { get; protected set; }
    protected float killingDistance;

    [Header("Audio Sources")]

    [SerializeField] protected AudioSource guardScream;
    [SerializeField] protected AudioSource guardKillingScream;
    [SerializeField] protected AudioSource guardWalkSound;

    [SerializeField] protected AudioSource[] enemyScreams;

    [SerializeField] protected InputActionsSO inputActions;

    protected virtual void Start()
    {
        isChasingPlayer = false;
        killingDistance = Random.Range(minkillingDistance, maxKillingDistance);
        EnemySpeed = Random.Range(minEnemySpeed, maxEnemySpeed);
        agent.speed = EnemySpeed;
        StartCoroutine(ChasePlayer());
    }

    protected virtual void GoToPlayer(float Radius)
    {
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        Vector2 randomCircle = Random.insideUnitCircle * Radius;
        Vector3 randomPosition = targetPosition + new Vector3(randomCircle.x, 0, randomCircle.y);
        agent.SetDestination(randomPosition);
    }
    protected abstract IEnumerator ChasePlayer();

    protected void GuardKill()
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

    protected IEnumerator standByPlayer()
    {
        while (true)
        {
            transform.position = GameManager.Instance.Player.transform.position;

            yield return null;
        }
    }
    protected void SpawnAtPosition()
    {
 
        Vector3 playerDirection = GameManager.Instance.Player.transform.position - transform.position;
        Vector3 spawnPosition = GameManager.Instance.Player.transform.position + playerDirection.normalized * 5;
        transform.position = spawnPosition;
    }
    protected void standInFronOfGhost()
    {
        if (GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost)
        {
            animator.SetTrigger("IsStanding");
            SpawnAtPosition();
            agent.isStopped = true;
            canKillPlayer = false;
        }
        else
        {
            agent.isStopped = false;
            canKillPlayer = true;
            animator.SetTrigger("IsWalking");
        }
    }

} 
