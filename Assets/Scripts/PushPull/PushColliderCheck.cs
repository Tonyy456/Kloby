using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushColliderCheck : MonoBehaviour
{
    public GameObject callerObject = null;
    public float pushForce = 100f;
    public void Start()
    {
        StartCoroutine(StartDisappear());
    }
    private IEnumerator StartDisappear()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered");
        if (collision.gameObject.tag == "Player" && collision.gameObject != callerObject)
        {
            Vector2 direction = collision.gameObject.transform.position - callerObject.transform.position;
            direction.Normalize();
            collision.gameObject.AddComponent<PushBehavior>().Initialize(direction, pushForce, 1f);
            Destroy(this.gameObject);
        }
    }
}
