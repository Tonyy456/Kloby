using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game
{

    //Delegates
    public delegate void PointChangeEvent();
    public static PointChangeEvent OnPointChange;
    public delegate void GameOver();
    public static GameOver OnGameOver;

    public static bool GameRunning = false;
    public static float countdown = 0f;
    public static float gameTime = 360f;

    public static string bluePlayer = "";
    public static string redPlayer = "";
    public static Player leftPlayer = Resources.Load("Players/BluePlayer") as Player;
    public static Player rightPlayer = Resources.Load("Players/GreenPlayer") as Player;
    public static GameObject ropeObject;

    public static void GameEnded()
    {
        OnGameOver();
        PointChange();
    }

    public static void PointChange()
    {
        GameRunning = false;
        countdown = 5f;
        if (OnPointChange != null)
        {
            OnPointChange();
        }
    }
}
