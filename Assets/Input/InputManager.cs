using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject PushColliderCheck;
    [SerializeField] private GameObject Rope;
    [SerializeField] private CharacterController blue;
    [SerializeField] private CharacterController red;

    /*    private PushPullHandler ppHandler;*/
    private GameInput gameInput;
    public void Start()
    {
        Game.ropeObject = Rope;
        GameInput game = new GameInput();
        InitializeWindows(game);
        this.gameInput = game;
    }

    private void QuitAction_performed(InputAction.CallbackContext obj)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void InitializeWindows(GameInput game)
    {

        /*
        blue.SetInputAction(
            player1.Move,
            player1.Boost);
        blue.InitializeInput(
            player1.Push,
            player1.Pull,
            PushColliderCheck);

        red.SetInputAction(
            player2.Move,
            player2.Boost);
        red.InitializeInput(
            player2.Push,
            player2.Pull,
            PushColliderCheck);
        */
    }

    public void Update()
    {     
        if (!Game.GameRunning)
        {
            Game.countdown -= Time.deltaTime;
            if (Game.countdown < 0f)
                Game.GameRunning = true;
        }
    }
}
