using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBehavior : MonoBehaviour
{
    public GameObject pullObject;

    private const float rotationSpeed = 1000f;

    public void Update()
    {

        //Rotate player in direction of the ball
        Vector3 direction = pullObject.transform.position - this.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //Keep ball a certain distance away
        if(direction.magnitude > 1.5)
            pullObject.transform.position = Vector2.MoveTowards(pullObject.transform.position, transform.position, 0.01f);
    }
}
