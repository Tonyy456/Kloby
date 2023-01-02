using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform toFollow;
    [SerializeField] private float speed;

    public void Register(GameObject go)
    {
        toFollow = go.transform;
        go.GetComponent<PlayerController2DTD>().Register(this.gameObject.GetComponent<Camera>());
    }

    public void Update()
    {
        if(toFollow != null)
        {
            float oldZ = transform.position.z;
            transform.position = Vector2.Lerp(transform.position, toFollow.position, Time.deltaTime * speed);
            this.transform.position = new Vector3(transform.position.x, transform.position.y, oldZ);
        }
    }
}
