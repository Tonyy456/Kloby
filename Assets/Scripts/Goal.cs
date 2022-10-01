using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            Debug.Log("GOOOAAAAL!");
        }

    }
}
