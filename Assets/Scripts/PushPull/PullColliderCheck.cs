using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullColliderCheck : MonoBehaviour
{
    public GameObject callerObject = null;
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
        if (collision.gameObject.tag == "Ball" && callerObject.GetComponent<PullBehavior>() == null)
        {
            callerObject.gameObject.AddComponent<PullBehavior>().pullObject = collision.gameObject;
        }
    }
}
