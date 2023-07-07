using System.Collections;
using UnityEngine;

public class SuitMan : Enemy
{
    [SerializeField] private Transform GoToPos;
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

            yield return new WaitForSeconds(5);

            agent.SetDestination(GoToPos.position);
        }

    }

}
