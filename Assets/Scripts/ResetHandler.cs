using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHandler : MonoBehaviour
{
    [SerializeField] private List<CharacterController> charReset;
    [SerializeField] private List<GameObject> posReset;

    private List<Vector3> startPositions = new List<Vector3>();
    private List<Quaternion> startRotations = new List<Quaternion>();
    public void Start()
    {
        for (int i = 0; i < posReset.Count; i++)
        {
            var item = posReset[i];
            startPositions.Add(item.transform.position);
            startRotations.Add(item.transform.rotation);
        }

    }
    public void DoDebug()
    {
        Debug.Log("Debugging!");
    }

    public void Reset()
    {
        foreach(var charCon in charReset)
        {
            Destroy(charCon.PullBehavior);
        }
        for (int i = 0; i < posReset.Count; i++)
        {
            var item = posReset[i];
            var rb = item.GetComponent<Rigidbody2D>();
            if (rb)
                rb.velocity = Vector2.zero;
            item.transform.position = startPositions[i];
            item.transform.rotation = startRotations[i];
        }
    }
}
