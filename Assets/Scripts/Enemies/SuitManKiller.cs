using System.Collections;
using UnityEngine;

public class SuitManKiller : Enemy
{
    protected override void GoToPlayer()
    {
        animator.SetBool("IsWalking", true);

        base.GoToPlayer();

    }
    protected override IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;

        while (isChasingPlayer)
        {
            GoToPlayer();

            yield return new WaitForSeconds(1);

            yield return base.ChasePlayer();
        }

    }

}
