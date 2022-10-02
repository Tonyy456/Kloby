using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonSelectable : MonoBehaviour
{
    private Color savedColor;
    private float fontsize;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        savedColor = text.color;
        fontsize = text.fontSize;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mouse inside");
        text.color = Color.red;
        text.fontSize += 40;
    }

    private void OnMouseExit()
    {
        text.fontSize = fontsize;
        text.color = savedColor;
    }
}
