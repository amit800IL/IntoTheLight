using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonEnemy : Enemy
{
    [SerializeField] private SkinnedMeshRenderer suitManRenderer;

    protected override void GoToPlayer(float Radius)
    {
        animator.SetTrigger("IsWalking");

        base.GoToPlayer(Radius);
    }

  
    protected override IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;

        yield return new WaitForSeconds(5);

        for (int i = 0; i < 2; i++)
        {
            animator.SetTrigger("IsWalking");
            SpawnAtPosition();

            guardWalkSound.Play();

            AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            enemyLight.enabled = false;

            animator.SetTrigger("IsWalking");
            SpawnAtPosition();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            animator.SetTrigger("IsWalking");
            SpawnAtPosition();

            suitManRenderer.forceRenderingOff = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            animator.SetTrigger("IsWalking");
            SpawnAtPosition();

            guardWalkSound.Stop();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            suitManRenderer.forceRenderingOff = false;

            enemyLight.enabled = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            guardWalkSound.Play();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            animator.SetTrigger("IsWalking");
            SpawnAtPosition();

            suitManRenderer.forceRenderingOff = true;

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
        suitManRenderer.forceRenderingOff = false;

        while (isChasingPlayer)
        {
            distance = Vector3.Distance(agent.transform.position, GameManager.Instance.Player.transform.position);

            guardWalkSound.Play();

            GoToPlayer(2.5f);

            standInFronOfGhost();

            yield return new WaitForSeconds(3);

            animator.SetTrigger("IsWalking");

            if (agent != null && distance < killingDistance && canKillPlayer)
            {
                GuardKill();

                yield return new WaitForSeconds(6);

                if (GameManager.Instance.playerStats.HP <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForSeconds(1);

    }
}
