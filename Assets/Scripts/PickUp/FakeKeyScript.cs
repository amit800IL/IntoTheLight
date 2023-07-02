using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FakeKeyScript : MonoBehaviour
{
    private bool pickUpAllowed = false;

<<<<<<< HEAD
=======
    [SerializeField] private Enemy guard;

>>>>>>> Fixes

    private void Update()
    {
        if (pickUpAllowed && Keyboard.current.eKey.isPressed)
        {
            PickUp();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;

<<<<<<< HEAD
            //Vector3 offset = Random.onUnitSphere * guard.Speed + guard.OffsetDistance;
=======
            Vector3 offset = Random.onUnitSphere * guard.enemyStats.EnemySpeed;
>>>>>>> Fixes

            //offset.y = 0;

            //guard.agent.Warp(GameManager.Instance.Player.transform.position + offset);
        }
    }

    private void PickUp()
    {
        Destroy(gameObject);
    }
}
