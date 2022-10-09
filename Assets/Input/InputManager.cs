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

    public void JoinP1()
    {

    }

    public void JoinP2()
    {

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
        var actions = game.Kloby;
        actions.Quit.performed += QuitAction_performed;
        actions.Quit.Enable();
        
        blue.SetInputAction(
            actions.WASDMovement,
            actions.WASDboost);
        blue.InitializeInput(
            actions.WASDPush,
            actions.WASDPull,
            PushColliderCheck);

        red.SetInputAction(
            actions.ArrowsMovement,
            actions.Arrowsboost);
        red.InitializeInput(
            actions.ArrowsPush,
            actions.ArrowsPull,
            PushColliderCheck);
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
