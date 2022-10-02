using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> toreset;

    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();
    public void Start()
    {
        for (int i = 0; i < toreset.Count; i++)
        {
            var item = toreset[i];
            positions.Add(item.transform.position);
            rotations.Add(item.transform.rotation);
        }

    }

    public void Reset()
    {
        foreach(var item in toreset)
        {
            Destroy(item.GetComponent<PushBehavior>());
            Destroy(item.GetComponent<PullBehavior>());
        }
        for (int i = 0; i < toreset.Count; i++)
        {
            var item = toreset[i];
            item.transform.position = positions[i];
            item.transform.rotation = rotations[i];
        }
    }
}
