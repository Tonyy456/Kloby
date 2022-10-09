using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestThing : MonoBehaviour
{
    private static int playersJoined = 0;
    private bool one = true;
    public void Start()
    {
        playersJoined += 1;
        if (playersJoined == 2)
        {
            GetComponent<PlayerInput>().SwitchCurrentActionMap("KlobyController2");
            one = false;
        }  else
        {
        }
    }
    public void DoDebug()
    {
        if (one)
        {
            Debug.Log("Controller1");
        } else
        {
            Debug.Log("Controller2");
        }        
    }
    public void Debug1()
    {
        Debug.Log("Controller1");
    }
    public void Debug2()
    {
        Debug.Log("Controller2");
    }
}
