using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInField : MonoBehaviour
{
    public void OnTrigg(Collider2D collision)
    {
        collision.gameObject.transform.position = collision.ClosestPoint(collision.gameObject.transform.position);
        Debug.Log("stuff");
    }
}
