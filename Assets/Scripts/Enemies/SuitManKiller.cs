using System.Collections;
using UnityEngine;

public class SuitManKiller : Enemy
{
    [SerializeField] private SkinnedMeshRenderer suitManRenderer;

    protected override void Start()
    {
        canKillPlayer = false;
        isChasingPlayer = false;
        base.Start();
    }
    protected override void GoToPlayer()
    {
        animator.SetBool("IsWalking", true);

        base.GoToPlayer();

    }
    protected override IEnumerator ChasePlayer()
    {
        Vector3 offset = new Vector3(5, 0, 0);

        yield return new WaitForSeconds(5);

        for (int i = 0; i < 2; i++)
        {
            GoToPlayer();

            guardFlySound.Play();

            guardScream.Play();

            enemyLight.enabled = false;

            transform.position = GameManager.Instance.Player.transform.position + offset;

            yield return new WaitForSeconds(5);

            transform.position = GameManager.Instance.Player.transform.position + offset;

            suitManRenderer.forceRenderingOff = true;

            guardScream.Stop();

            yield return new WaitForSeconds(5);

            transform.position = GameManager.Instance.Player.transform.position + offset;

            guardFlySound.Stop();

            guardScream.Play();

            suitManRenderer.forceRenderingOff = false;

            enemyLight.enabled = true;

            yield return new WaitForSeconds(5);

            guardFlySound.Play();

            guardScream.Play();

            suitManRenderer.forceRenderingOff = true;

            enemyLight.enabled = false;
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
