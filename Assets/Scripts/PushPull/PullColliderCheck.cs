using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullColliderCheck : MonoBehaviour
{
    public GameObject callerObject = null;
    public bool flip = false;
    public void Start()
    {
        StartCoroutine(StartDisappear());
    }
    private IEnumerator StartDisappear()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball" && callerObject.GetComponent<PullBehavior>() == null)
        {
            var comp = callerObject.gameObject.AddComponent<PullBehavior>();
            comp.pullObject = collision.gameObject;
            comp.flip = flip;
            Debug.Log(callerObject.name);
        }
    }
}
