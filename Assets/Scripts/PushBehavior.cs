using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBehavior : MonoBehaviour
{
    private Vector2 PushForce;
    private float pushScale;
    private float time;

    public void Initialize(Vector2 force, float scale, float time)
    {
        PushForce = force;
        pushScale = scale;
        this.time = time;
    }

    void Update()
    {
        if (time <= 0)
            Destroy(this);
        time = time - Time.deltaTime;
        this.transform.Translate(PushForce * pushScale, Space.World);
    }
}
