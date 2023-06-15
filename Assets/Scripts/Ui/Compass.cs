using UnityEngine;

public class Compass : MonoBehaviour
{
    private Vector3 positionToLocate;

    [SerializeField] Transform[] doors;

    private void Update()
    {
        LocateDoor();
    }

    private void LocateDoor()
    {
        foreach (Transform door in doors)
        {
            Vector3 direction = (transform.position - GameManager.Instance.Player.transform.position).normalized;
            float Angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;   
        }
    }
}
