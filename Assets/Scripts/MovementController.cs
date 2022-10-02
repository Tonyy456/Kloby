using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    private InputAction movementAction;

    public void SetInputAction(InputAction input)
    {
        movementAction = input;
        movementAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 speedVector = movementAction.ReadValue<Vector2>();
        if (GetComponent<PullBehavior>() == null && speedVector != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * speedVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        this.transform.Translate(speedVector * speed * Time.deltaTime, Space.World);

    }
}
