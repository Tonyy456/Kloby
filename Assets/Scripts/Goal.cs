using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private float timer = 5f;
    private float timeLeft = 0f;
    private static int goalCounter = 0;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(timeLeft <= 0)
        {
            timeLeft = timer;
            test();
        }
    }

    public void Update()
    {
        timeLeft -= Time.deltaTime;
    }

    public void Start()
    {
        Debug.Log("started");
        GoalEvent.OnGoal += test;
    }

    private void test()
    {
        goalCounter++;
    }
}
