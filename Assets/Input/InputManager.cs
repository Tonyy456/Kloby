using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject bluePlayer;
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject PushColliderCheck;
    [SerializeField] private GameObject Rope;

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
        var actions = game.Kloby;
        actions.Quit.performed += QuitAction_performed;
        actions.Quit.Enable();
        bluePlayer.GetComponent<MovementController>().SetInputAction(
            actions.WASDMovement,
            actions.WASDboost);
        bluePlayer.GetComponent<CharacterActionController>().InitializeInput(
            actions.WASDPush,
            actions.WASDPull,
            PushColliderCheck);


        redPlayer.GetComponent<MovementController>().SetInputAction(
            actions.ArrowsMovement,
            actions.Arrowsboost);
        redPlayer.GetComponent<CharacterActionController>().InitializeInput(
            actions.ArrowsPush,
            actions.ArrowsPull,
            PushColliderCheck);
    }

    private void InitializeMacos(GameInput game)
    {
        var actions = game.KlobyMacOS;
        bluePlayer.GetComponent<MovementController>().SetInputAction(
            actions.WASDMovement,
            actions.WASDboost);
        bluePlayer.GetComponent<CharacterActionController>().InitializeInput(
            actions.WASDPush,
            actions.WASDPull,
            PushColliderCheck);


        redPlayer.GetComponent<MovementController>().SetInputAction(
            actions.ArrowsMovement,
            actions.Arrowsboost);
        redPlayer.GetComponent<CharacterActionController>().InitializeInput(
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
