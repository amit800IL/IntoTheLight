using UnityEngine;

public class InRoomBehavior : MonoBehaviour
{
    public bool isPlayerInsideRoom { get; private set; } = false;
    [SerializeField] private AudioSource hitDoor;
    [SerializeField] private Enemy enemy;

    private void Update()
    {
        enemyAttackDoor();
    }
    private void enemyAttackDoor()
    {
        float enemyTriggerDistance = Vector3.Distance(transform.position, enemy.transform.position);

        if (enemyTriggerDistance < 50 && isPlayerInsideRoom)
        {
            hitDoor.Play();
            enemy.guardWalkSound.Stop();
            enemy.agent.isStopped = true;
            enemy.animator.ResetTrigger("IsWalking");
            enemy.animator.SetTrigger("IsAttacking");
        }
        else if (!isPlayerInsideRoom)
        {
            hitDoor.Stop();
            enemy.guardWalkSound.Play();
            enemy.agent.isStopped = false;
            enemy.animator.ResetTrigger("IsAttacking");
            enemy.animator.SetTrigger("IsWalking");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInsideRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInsideRoom = false;
        }
    }
}
