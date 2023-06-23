using UnityEngine;

public class Compass : MonoBehaviour
{
    private Vector2 positionToLocate;

    private float Closestdistance = Mathf.Infinity;

    private Transform closestDoor;

    [SerializeField] private Transform[] doors;

    [SerializeField] private GameObject arrow;

    private void Update()
    {
        LocateDoor();
        UpdateDoorLocation();
    }

    private void LocateDoor()
    {
        Closestdistance = Mathf.Infinity;
        closestDoor = null;

        foreach (Transform door in doors)
        {
            float Distance = Vector3.Distance(GameManager.Instance.PlayerMovement.transform.position, door.transform.position);
            //Debug.Log(Distance);

            if (Distance < Closestdistance)
            {
                Closestdistance = Distance;

                closestDoor = door;
            }

        }
    }

    private void UpdateDoorLocation()
    {
        if (closestDoor != null && Closestdistance > 5 && Closestdistance < 30)
        {
            positionToLocate = (arrow.transform.position - closestDoor.transform.position).normalized;
            float angle = Mathf.Atan2(positionToLocate.y, positionToLocate.x) * Mathf.Rad2Deg;
            Quaternion rotation = GameManager.Instance.PlayerMovement.transform.rotation;
            arrow.transform.rotation = Quaternion.Euler(0, 0, -angle) * rotation;
        }
    }
}
