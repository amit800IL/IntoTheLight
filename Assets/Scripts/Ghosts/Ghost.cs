using UnityEngine;
using UnityEngine.InputSystem;

public class Ghost : MonoBehaviour
{

    [SerializeField] private Light Light;

    private void Start()
    {
      
        InputManager.Instance.OnGhostAwakening.AddListener(OnPlayerAwakeGhost);
    }
    public void OnPlayerAwakeGhost()
    {
  
            if (GameManager.Instance.PlayerGhostAwake !=null && Vector3.Distance(GameManager.Instance.PlayerGhostAwake.transform.position, transform.position) < 0.5f)
            {
                InputManager.Instance.GetButtonPressValue();
                Light.spotAngle = 100f;
                Light.intensity = 70;
            }
            else
            {
                Light.spotAngle = default;
                Light.intensity = default;
            }
        

    }

    public void SetPlayer(PlayerGhostAwake player)
    {
        GameManager.Instance.PlayerGhostAwake = player;
    }






}
