using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBehavior : MonoBehaviour
{
    private Vector2 pushForce;
    private float pushScale;
    private float time;

    public void Initialize(Vector2 force, float scale, float time)
    {
        pushForce = force;
        pushScale = scale;
        this.time = time;
    }

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        if(rb)
        {
            rb.AddForce(pushForce * pushScale);
        }
        Destroy(this);
    }
}
