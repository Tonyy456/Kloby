using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    [SerializeField] private float kickForce = 2f;
    [SerializeField] private Transform playerBody;

    [Header("ADVANCED")]
    [SerializeField] private float minClampDistance = 0.1f;
    [SerializeField] private float maxClampDistance = 2f;

    private Animator animator;
    public void Start()
    {
        animator = playerBody.GetComponent<Animator>();
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && animator.GetCurrentAnimatorStateInfo(0).IsName("Swipe"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 force = collision.gameObject.transform.position - playerBody.position;
            float distance = force.magnitude;
            float scale = 1 / Mathf.Pow(Mathf.Clamp(distance, minClampDistance, maxClampDistance),2);
            rb.AddForce((collision.gameObject.transform.position - playerBody.position) * kickForce * scale);
        }
        
    }
}
