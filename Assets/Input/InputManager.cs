using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject bluePlayer;
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject dotPrefab;

    private PushPullHandler ppHandler;
    private GameInput gameInput;
    public void Start()
    {
        gameInput = new GameInput();
        ppHandler = new PushPullHandler(gameInput.FootballActions.Push, gameInput.FootballActions.Pull)
        {
            User1 = redPlayer,
            User2 = bluePlayer,
            prefab = dotPrefab
        };

        bluePlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.FootballActions.Arrows);
        redPlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.FootballActions.WASD);
    }
}
