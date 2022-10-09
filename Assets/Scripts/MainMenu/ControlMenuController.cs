using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlMenuController : MonoBehaviour
{
    [SerializeField] private List<Vector3> BallSpots;
    [SerializeField] private GameObject ball;
    public int index = 0;

    public void Up()
    {
        index += 1;
        index %= BallSpots.Count;
        Game.Keyboard = index;
        ball.transform.localPosition = BallSpots[index];
    }

    public void Down()
    {
        index -= 1;
        if (index < 0)
        {
            index += BallSpots.Count;
        }
        Game.Keyboard = index;
        ball.transform.localPosition = BallSpots[index];
    }


}
