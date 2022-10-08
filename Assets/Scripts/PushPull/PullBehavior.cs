using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBehavior : MonoBehaviour
{
    public GameObject pullObject;
    private const float rotationSpeed = 1000f;
    [SerializeField] private float scaleY = 0.08f;
    [SerializeField] private float scaleX = 1f;

    private GameObject rope;
    public void Start()
    {
        rope = Instantiate(Game.ropeObject);
        rope.GetComponent<SpriteRenderer>().sortingOrder = 100;
    }

    public void OnDisable()
    {
        Destroy(rope);
    }

    public void Update()
    {
        bool flip = GetComponent<MovementController>().flipRotation;
        Vector3 forward = Vector3.forward;
        if (flip) forward = -forward;

        //Rotate player in direction of the ball
        Vector3 direction = pullObject.transform.position - this.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(forward, Quaternion.Euler(0, 0, 90) * direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        //Rotate ball in direction of player pulling
        Debug.Log("Rotation before: " + pullObject.transform.rotation);
        pullObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,0.01f);


        //Keep ball a certain distance away
        if (direction.magnitude > 1.5)
            pullObject.transform.position = Vector2.MoveTowards(pullObject.transform.position, transform.position, 0.1f);

        // update rope
        rope.transform.position = this.transform.position + direction / 2;
        rope.transform.localScale = new Vector3(direction.magnitude * scaleX, scaleY, 0);
        rope.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 0.001f * Time.deltaTime);
    }
}
