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
            

            Transform target = User1.transform;
            Vector2 origin = target.position;
            Vector2 forward = target.up * 10;
            Debug.DrawRay(origin, forward, Color.black, 2f, false);

            RaycastHit2D hit = Physics2D.Raycast(origin, forward);
            Debug.Log("" + origin + "    " + forward);
            if (hit.collider != null && hit.collider.gameObject != User1)
            {
                Debug.Log(hit.collider.gameObject.name);
                Debug.Log("Pushing user1 away");
            }
        }

    }

    private void PullUser1(InputAction.CallbackContext context)
    {
        if (User1 != null)
        {
            Debug.Log("User1 pulling ball");
            GameObject result = GameObject.Instantiate(prefab);
            result.transform.position = User1.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * User1.transform.up.normalized * 2;
        }
    }


}
