using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float sprintspeed = 4;
    [SerializeField] private float rotationSpeed;
    public bool flipRotation = false;
    private InputAction movementAction;
    private float movespeed;

    public void SetInputAction(InputAction input, InputAction boost)
    {
        movementAction = input;
        movementAction.Enable();

        boost.Enable();
        boost.performed += Boost;
        boost.canceled += Unboost;

        movespeed = speed;
    }

    void Boost(InputAction.CallbackContext context)
    {
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(scale.x, scale.y * 6f/7f, scale.z);
        movespeed = sprintspeed;
    }

    void Unboost(InputAction.CallbackContext context)
    {
        Vector3 scale = this.transform.localScale;
        this.transform.localScale = new Vector3(scale.x, scale.y * 7f/6f, scale.z);
        movespeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        if (!Game.GameRunning) return;

        Vector2 speedVector = movementAction.ReadValue<Vector2>();
        if (GetComponent<PullBehavior>() == null && speedVector != Vector2.zero)
        {
            Vector3 forward = Vector3.forward;
            if(flipRotation) forward = -forward;
            Quaternion toRotation = Quaternion.LookRotation(forward, Quaternion.Euler(0, 0, 90) * speedVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        var pb = GetComponent<PullBehavior>();
        bool moveAllowed = true;
        if(pb != null)
        {
            var cpos = this.transform.position;
            Vector2 target = new Vector2(cpos.x, cpos.y) + speedVector * speed * Time.deltaTime;
            var newPos = Vector3.MoveTowards(this.transform.position, target, 22f);
            var newDistance = (newPos - pb.pullObject.transform.position).magnitude;
            if (newDistance > 3)
                moveAllowed = false;
        }

        if(moveAllowed)
            this.transform.Translate(speedVector * movespeed * Time.deltaTime, Space.World);

        

    }
}
