using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour
{

    [Header("General")]

    protected bool isChasingPlayer;
    protected float distance;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] protected Light enemyLight;
    [SerializeField] protected Animator animator;

    [Header("Player Refernces")]

    [SerializeField] protected PlayerMovement playerMovement;
    [SerializeField] protected PlayerStats playerStats;

    [Header("Numbers")]

    [SerializeField] public EnemyStatsSO enemyStats;

    [Header("Audio Sources")]

    [SerializeField] protected PlayerVoice playerVoice;
    [SerializeField] protected AudioSource guardKillingScream;
    [SerializeField] protected AudioSource guardFlySound;

    protected void Start()
    {
        StartCoroutine(ChasePlayer());
    }

    protected virtual void GoToPlayer()
    {
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = transform.position.y;
        agent.SetDestination(targetPosition);
    }
    protected virtual IEnumerator ChasePlayer()
    {

        enemyLight.enabled = true;
        isChasingPlayer = true;
        guardFlySound.Play();

        while (isChasingPlayer)
        {
            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            GoToPlayer();

            if (agent != null && distance < enemyStats.killingDistance && enemyStats.canKillPlayer)
            {
                GuardKill();

                yield return new WaitForSeconds(1);

                if (playerStats.HP <= 0)
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
        Camera.main.transform.LookAt(transform.position);
        guardKillingScream.Play();
        guardKillingScream.volume = 1f;
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = transform.position.y;
        Camera.main.transform.LookAt(targetPosition);
        agent.isStopped = true;
        playerStats.HP -= 100;
        playerVoice.playerScream.Play();
        playerVoice.secondPlayerScream.Play();
    }

}
