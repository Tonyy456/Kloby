using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerInputManager manager;
    public void Start()
    {
        /*
         * public void JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, 
         * string controlScheme = null, InputDevice pairWithDevice = null);
         */
        int i = 0;
        foreach(var controller in Gamepad.all)
        {
            manager.JoinPlayer(i, -1, "KlobyController"+i, controller);
            i++;
        }
        
    }
}
