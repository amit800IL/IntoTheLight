using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Transform Camera;

    private void Update()
    {
        Player = Camera;
    }
}
