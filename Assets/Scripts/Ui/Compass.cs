using UnityEngine;

public class Compass : MonoBehaviour
{
    private Vector3 positionToLocate;

    private float distance;

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
            distance = Vector3.Distance(arrow.transform.position, door.transform.position);

            Debug.Log(distance);

            if (distance > 5 && distance < 30)
            {
                positionToLocate = (arrow.transform.position - door.transform.position).normalized;
                float Angle = Mathf.Atan2(positionToLocate.x, positionToLocate.z) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(Angle, 0, 0);
            }
        }
    }
}
