using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;


    private float width;
    private float height;
    private Vector3 center;
    public void Start()
    {
        Camera cam = GetComponent<Camera>();
        center = this.transform.position;
        height = cam.orthographicSize * 2f;
        width = cam.aspect * height;
    }

    public void Update()
    {
        foreach(GameObject item in objects)
        {
            float x = Mathf.Clamp(item.transform.position.x, center.x - width / 2, center.x + width / 2);
            float y = Mathf.Clamp(item.transform.position.y, center.y - height / 2, center.y + height / 2);
            item.transform.position = new Vector3(x, y, item.transform.position.z);
        }
    }
}
