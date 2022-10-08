using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuHandler : MonoBehaviour
{
    [SerializeField] private int maxStringLength = 5;
    public void SetGameTime(float time)
    {
        Game.gameTime = time;
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
