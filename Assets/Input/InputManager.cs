using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject bluePlayer;
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject PushColliderCheck;

    public static bool GameRunning = false;
    public static float countdown = 5f;

/*    private PushPullHandler ppHandler;*/
    private GameInput gameInput;
    public void Start()
    {
        gameInput = new GameInput();

        bluePlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.Kloby.WASDMovement,
            gameInput.Kloby.WASDboost);
        bluePlayer.GetComponent<CharacterActionController>().InitializeInput(
            gameInput.Kloby.WASDPush,
            gameInput.Kloby.WASDPull,
            PushColliderCheck);


        redPlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.Kloby.ArrowsMovement,
            gameInput.Kloby.Arrowsboost);
        redPlayer.GetComponent<CharacterActionController>().InitializeInput(
            gameInput.Kloby.ArrowsPush,
            gameInput.Kloby.ArrowsPull,
            PushColliderCheck);

        PointChangeHandler.OnPointChange += ResetCD;
    }

    public void ResetCD()
    {
        GameRunning = false;
        countdown = 5f;
    }

    public void Update()
    {     
        if (!GameRunning)
        {
            countdown -= Time.deltaTime;
            if (countdown < 0f)
                GameRunning = true;
        }

    }
}
