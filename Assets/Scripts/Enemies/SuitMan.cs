using System.Collections;
using UnityEngine;

public class SuitMan : Enemy
{
    protected override void GoToPlayer()
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsSitting", false);

        base.GoToPlayer();

    }
    protected override IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;

        while (isChasingPlayer)
        {
            
            ScarePlayer();

            yield return new WaitForSeconds(2);
            
            GoToPlayer();

            yield return base.ChasePlayer();

        }

    }

    private void ScarePlayer()
    {
        animator.SetBool("IsSitting", true);
        animator.SetBool("IsWalking", false);
        guardKillingScream.Play();
    }
}
