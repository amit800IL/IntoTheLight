using System.Collections;
using UnityEngine;

public class SuitMan : Enemy
{
    [SerializeField] private SkinnedMeshRenderer suitManRenderer;

    protected override void GoToPlayer()
    {
        int randomIndex = Random.Range(0, enemyAnimations.Length);
        string randomAnimation = enemyAnimations[randomIndex];
        animator.SetBool(randomAnimation, true);

        base.GoToPlayer();

    }
    protected override IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;

        Vector3 offset = new Vector3(8, 0, 0);

        yield return new WaitForSeconds(5);


        for (int i = 0; i < 2; i++)
        {
            GoToPlayer();

            guardFlySound.Play();

            AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            enemyLight.enabled = false;

            agent.transform.position = GameManager.Instance.Player.transform.position + offset;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            agent.transform.position = GameManager.Instance.Player.transform.position + offset;

            suitManRenderer.forceRenderingOff = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            agent.transform.position = GameManager.Instance.Player.transform.position + offset;

            guardFlySound.Stop();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            suitManRenderer.forceRenderingOff = false;

            enemyLight.enabled = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            guardFlySound.Play();


            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            suitManRenderer.forceRenderingOff = true;

            enemyLight.enabled = false;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

        }

        canKillPlayer = true;
        isChasingPlayer = true;
        enemyLight.enabled = true;
        suitManRenderer.forceRenderingOff = false;

        while (isChasingPlayer)
        {
            yield return base.ChasePlayer();
        }

    }
}
