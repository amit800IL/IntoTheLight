using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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

    protected float maxDelay = 1f;
    protected float minDelay = 1f;
    protected float minEnemySpeed = 5f;
    protected float maxEnemySpeed = 10f;
    protected float minkillingDistance = 3f;
    protected float maxKillingDistance = 6f;
    public float EnemySpeed { get; protected set; }
    protected float killingDistance;

    [Header("Audio Sources")]

    [SerializeField] protected AudioSource guardScream;
    [SerializeField] protected AudioSource guardKillingScream;
    [SerializeField] protected AudioSource guardFlySound;

    [SerializeField] protected AudioSource[] enemyScreams;
    [SerializeField] protected string[] enemyAnimations;

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
    protected virtual IEnumerator ChasePlayer()
    {

        while (isChasingPlayer)
        {
            distance = Vector3.Distance(agent.transform.position, GameManager.Instance.Player.transform.position);

            AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            GoToPlayer(8f);

            if (agent != null && distance < killingDistance && canKillPlayer)
            {
                GuardKill();

                yield return new WaitForSeconds(1);

                if (GameManager.Instance.playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);
    }

    protected void GuardKill()
    {
        Camera.main.transform.LookAt(agent.transform.position);
        guardKillingScream.Play();
        guardKillingScream.volume = 1f;
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = agent.transform.position.y;
        Camera.main.transform.LookAt(targetPosition);
        agent.isStopped = true;
        GameManager.Instance.playerStats.HP -= 200;
        PlayerVoiceManager.Instance.playerScream.Play();
        PlayerVoiceManager.Instance.secondPlayerScream.Play();
    }


}
