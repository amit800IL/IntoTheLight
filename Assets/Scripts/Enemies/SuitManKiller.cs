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

        yield return new WaitForSeconds(5);

        while (isChasingPlayer)
        {
            yield return base.ChasePlayer();
        }

    }

}
