using UnityEngine;

public class Compass : MonoBehaviour
{
    private Vector3 positionToLocate;

    [SerializeField] private Transform[] doors;

    [SerializeField] GameObject arrow;

    private void Update()
    {
        LocateDoor();
    }

    private void LocateDoor()
    {
        foreach (Transform door in doors)
        {
            Vector3 direction = (arrow.transform.position - door.transform.position).normalized;
            float Angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Angle, 0, 0);
        }
    }
}
