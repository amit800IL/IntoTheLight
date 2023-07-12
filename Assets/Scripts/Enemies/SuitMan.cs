using System.Collections;
using UnityEngine;

public class SuitMan : Enemy
{
    [SerializeField] private SkinnedMeshRenderer suitManRenderer;

    protected override void GoToPlayer(float Radius)
    {
        int randomIndex = Random.Range(0, enemyAnimations.Length);
        string randomAnimation = enemyAnimations[randomIndex];
        animator.SetBool(randomAnimation, true);

        base.GoToPlayer(Radius);

    }
    protected override IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;

        Vector3 offset = new Vector3(8, 0, 0);

        yield return new WaitForSeconds(5);


        for (int i = 0; i < 2; i++)
        {
            GoToPlayer(8f);

            guardFlySound.Play();

            AudioSource randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            enemyLight.enabled = false;

            GoToPlayer(8f);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            GoToPlayer(8f);

            suitManRenderer.forceRenderingOff = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            GoToPlayer(8f);

            guardFlySound.Stop();

            randomScream = enemyScreams[Random.Range(0, enemyScreams.Length)];
            randomScream.Play();

            suitManRenderer.forceRenderingOff = false;

            enemyLight.enabled = true;

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            guardFlySound.Play();

            GoToPlayer(8f);

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

        yield return base.ChasePlayer();


    }
}
