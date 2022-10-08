using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    private bool IsDisplayed = false;
    private void OnMouseUp()
    {
        IsDisplayed = !IsDisplayed;

        if(IsDisplayed)
        {
            text.text = "Blue player:\n" +
                "WASD to move\n" +
                "j to push the ball\n" +
                "k to pull a player away\n" +
                "l to sprint!\n\n" +
                "Red player:\n" +
                "Arrows to move\n" +
                "NUM0 to pull\n" +
                "NUM. to push\n" +
                "NUMEnter to sprint!\n";
        }
        else
        {
            text.text = "";
        }
    }
}
