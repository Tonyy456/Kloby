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
    public static float countdown = 3f;
    public static float gameTime = 120f;

    public static string bluePlayer = "";
    public static string redPlayer = "";
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
