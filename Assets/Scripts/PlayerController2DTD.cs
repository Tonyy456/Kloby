using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController2DTD : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;

    [SerializeField] private bool isOwner = false;
    private Camera cam;

    Animator animator;

    public void OnEnable()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach(GameObject b in balls)
        {
            Physics2D.IgnoreCollision(b.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Debug.Log("ignoring collision between player and: " + b.name);
        }     
    }
    public void Start()
    {
        animator = GetComponent<Animator>();  
    }

    public void Register(Camera cam)
    {
        this.cam = cam;
    }
    public void Update()
    {
        isOwner = IsOwner;
        if (!IsOwner) return;

        Vector3 keyDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) keyDirection.y = +1f;
        if (Input.GetKey(KeyCode.A)) keyDirection.x = -1f;
        if (Input.GetKey(KeyCode.S)) keyDirection.y = -1f;
        if (Input.GetKey(KeyCode.D)) keyDirection.x = +1f;
        if (keyDirection.magnitude > 0.01f) keyDirection.Normalize();

        transform.position += keyDirection * moveSpeed * Time.deltaTime;
        if(cam != null)
        {
            Vector3 rotateDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            RotateTo(rotateDirection, rotationSpeed);
        } 

        if(Input.GetMouseButtonDown(0))
        {
            animator.Play("Swipe");
        }
    }

    private void RotateTo(Vector3 facing, float speed)
    {
        Debug.DrawRay(transform.position, facing);
        facing.z = 0;
        Quaternion quaternionRotation = Quaternion.LookRotation(this.transform.forward, facing);
        transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternionRotation, 3 * speed * Time.deltaTime);
    }
}
