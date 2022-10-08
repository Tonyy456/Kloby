using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float speed = 3;
    [SerializeField] private float sprintspeed = 4;
    [SerializeField] private float rotationSpeed;
    public bool flipRotation = false;
    private InputAction movementAction;
    private float movespeed;
    private GameObject colliderChecker;

    public void SetInputAction(InputAction input, InputAction boost)
    {
        movespeed = speed;
        movementAction = input;

        boost.performed += Boost;
        boost.canceled += Unboost;

        movementAction.Enable();
        boost.Enable();
    }

    void Boost(InputAction.CallbackContext context)
    {
        Vector3 scale = playerObject.transform.localScale;
        playerObject.transform.localScale = new Vector3(scale.x, scale.y * 6f / 7f, scale.z);
        movespeed = sprintspeed;
    }

    void Unboost(InputAction.CallbackContext context)
    {
        Vector3 scale = playerObject.transform.localScale;
        playerObject.transform.localScale = new Vector3(scale.x, scale.y * 7f / 6f, scale.z);
        movespeed = speed;
    }

    public void InitializeInput(InputAction pushAction, InputAction pullAction, GameObject collider)
    {
        pushAction.performed += Push;
        pullAction.performed += Pull;
        pushAction.Enable();
        pullAction.Enable();
        colliderChecker = collider;

        Game.OnPointChange += CleanComponents;
    }

    private void CleanComponents()
    {
        Destroy(playerObject.GetComponent<PushBehavior>());
        Destroy(playerObject.GetComponent<PullBehavior>());
    }

    private void Push(InputAction.CallbackContext context)
    {
        if (playerObject.GetComponent<PushBehavior>() != null) return;
        GameObject outObj = GameObject.Instantiate(colliderChecker);
        outObj.AddComponent<PushColliderCheck>().callerObject = playerObject.gameObject;
        outObj.transform.position = playerObject.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * playerObject.transform.up.normalized;
    }

    private void Pull(InputAction.CallbackContext context)
    {
        var component = playerObject.GetComponent<PullBehavior>();
        if (component != null)
        {
            Destroy(component);
            return;
        }
        GameObject outObj = GameObject.Instantiate(colliderChecker);
        if (outObj.GetComponent<PullColliderCheck>() == null)
        {
            var comp = outObj.AddComponent<PullColliderCheck>();
            comp.callerObject = playerObject.gameObject;
            comp.flip = flipRotation;
        }

        outObj.transform.position = playerObject.transform.position + Quaternion.AngleAxis(-90, new Vector3(0, 0, 1)) * playerObject.transform.up.normalized;
    }

    public void Update()
    {
        if (!Game.GameRunning) return;
        Vector2 speedVector = movementAction.ReadValue<Vector2>();

        //Rotate player in direction of speed vector
        if (playerObject.GetComponent<PullBehavior>() == null && speedVector != Vector2.zero)
        {
            Vector3 forward = Vector3.forward;
            if (flipRotation) forward = -forward;
            Quaternion toRotation = Quaternion.LookRotation(forward, Quaternion.Euler(0, 0, 90) * speedVector);
            playerObject.transform.rotation = Quaternion.RotateTowards(playerObject.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        //make sure player cant move too far from rope
        var pb = playerObject.GetComponent<PullBehavior>();
        bool moveAllowed = true;
        if (pb != null)
        {
            var cpos = playerObject.transform.position;
            Vector2 target = new Vector2(cpos.x, cpos.y) + speedVector * speed * Time.deltaTime;
            var newPos = Vector3.MoveTowards(playerObject.transform.position, target, 22f);
            var newDistance = (newPos - pb.pullObject.transform.position).magnitude;
            if (newDistance > 3)
                moveAllowed = false;
        }

        if (moveAllowed)
            playerObject.transform.Translate(speedVector * movespeed * Time.deltaTime, Space.World);
    }

}
