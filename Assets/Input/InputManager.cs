using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject bluePlayer;
    [SerializeField] private GameObject redPlayer;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject PushColliderCheck;

/*    private PushPullHandler ppHandler;*/
    private GameInput gameInput;
    public void Start()
    {
        gameInput = new GameInput();

        bluePlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.Kloby.WASDMovement);
        bluePlayer.GetComponent<CharacterActionController>().InitializeInput(
            gameInput.Kloby.WASDPush,
            gameInput.Kloby.WASDPull,
            PushColliderCheck);


        redPlayer.GetComponent<MovementController>().SetInputAction(
            gameInput.Kloby.ArrowsMovement);
        redPlayer.GetComponent<CharacterActionController>().InitializeInput(
            gameInput.Kloby.ArrowsPush,
            gameInput.Kloby.ArrowsPull,
            PushColliderCheck);
    }
}
