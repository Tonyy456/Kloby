using UnityEngine.InputSystem;
using UnityEngine;

public class PushPullHandler
{
    private InputAction push;
    private InputAction pull;

    public GameObject prefab { get; set; } = null;
    public GameObject User1 { get; set; } = null;
    public GameObject User2 { get; set; } = null;

    public PushPullHandler(InputAction push, InputAction pull)
    {
        push.performed += PushUser1;
        pull.performed += PullUser1;
        push.Enable();
        pull.Enable();
    }

    private void PushUser1(InputAction.CallbackContext context)
    {
        if (User1 != null)
        {
            Debug.Log("Pushing user1 away");
            GameObject result = GameObject.Instantiate(prefab);
            result.transform.position = User1.transform.position + Quaternion.AngleAxis(-90, new Vector3(0,0,1)) * User1.transform.up * 2;
        }

    }

    private void PullUser1(InputAction.CallbackContext context)
    {
        if (User1 != null)
        {
            Debug.Log("User1 pulling ball");
            GameObject result = GameObject.Instantiate(prefab);
            result.transform.position = User1.transform.position + Quaternion.Euler(0, 0, -180) * User1.transform.forward * 20;
        }
    }


}
