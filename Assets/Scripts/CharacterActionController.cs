using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterActionController : MonoBehaviour
{

    private GameObject colliderChecker;
    public void InitializeInput(InputAction pushAction, InputAction pullAction, GameObject collider)
    {
        pushAction.performed += Push;
        pushAction.Enable();
        pullAction.performed += Pull;
        pullAction.Enable();
        colliderChecker = collider;
    }

    private void Push(InputAction.CallbackContext context)
    {
        if (GetComponent<PushBehavior>() != null) return;
        GameObject outObj = GameObject.Instantiate(colliderChecker);
        outObj.AddComponent<PushColliderCheck>().callerObject = this.gameObject;
        outObj.transform.position = transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * transform.up.normalized;
    }

    private void Pull(InputAction.CallbackContext context)
    {
        var component = GetComponent<PullBehavior>();
        if(component != null)
        {
            Destroy(component);
            return;
        }
        GameObject outObj = GameObject.Instantiate(colliderChecker);
        outObj.AddComponent<PullColliderCheck>().callerObject = this.gameObject;
        outObj.transform.position = transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * transform.up.normalized;
    }
}
