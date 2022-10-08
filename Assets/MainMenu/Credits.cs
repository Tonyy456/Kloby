using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    private bool IsDisplayed = false;
    private void OnMouseUp()
    {
        IsDisplayed = !IsDisplayed;

        if(IsDisplayed)
        {
            text.text = "Programming: Tony D'Alesandro\n" +
                "Art: Kloe Kuhn\n" +
                "Sound: Did not have time to implement!!\n" +
                "Venmo: Anthony_Dalesandro_3.....";
        }
        else
        {
            text.text = "";
        }
    }
}
