using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionsMenuHandler : MonoBehaviour
{
    [SerializeField] private int maxStringLength = 5;
    [SerializeField] private TMPro.TMP_InputField gameTimeText;
    [SerializeField] private TMPro.TMP_InputField textField;
    public void SetGameTime()
    {
        try
        {
            
            float time = Int32.Parse(gameTimeText.text);
            time = Mathf.Clamp(time, 20, 360);
            Game.gameTime = time;
        }
        catch (FormatException) { }
    }

    public void SetResetTime()
    {
        try
        {

            float time = Int32.Parse(textField.text);
            time = Mathf.Clamp(time, 0, 10);
            Game.countdown = time;
        }
        catch (FormatException) { }
    }

    public void SetBlueName(string name)
    {
        if (name.Length > maxStringLength)
            Game.bluePlayer = name.Substring(0, maxStringLength);
        else
            Game.bluePlayer = name;
    }

    public void SetRedName(string name)
    {
        if (name.Length > maxStringLength)
            Game.redPlayer = name.Substring(0, maxStringLength);
        else
            Game.redPlayer = name;
    }

}
