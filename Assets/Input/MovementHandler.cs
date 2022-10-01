using UnityEngine.InputSystem;
using UnityEngine;

public class MovementHandler
{
    private GameObject redPlayer;
    private GameObject bluePlayer;
    private InputAction redInput;
    private InputAction blueInput;

    public MovementHandler(InputAction redInput, InputAction blueInput)
    {
        this.redInput = redInput;
        this.blueInput = blueInput;
        redInput.Enable();
        blueInput.Enable();
    }

    public void SetRed(GameObject redPlayer) =>
        this.redPlayer = redPlayer;

    public void SetBlue(GameObject bluePlayer) =>
        this.bluePlayer = bluePlayer;

    private void MoveRed(InputAction.CallbackContext context)
    {
        Vector2 movementDirection = redInput.ReadValue<Vector2>();
        //redPlayer.transform.Translate(movementDirection, Space.Self);
        MovementController ctrl = redPlayer.GetComponent<MovementController>();
        //ctrl.UpdateSpeed(movementDirection);
    }

    private void MoveBlue(InputAction.CallbackContext context)
    {
        Debug.Log(blueInput.ReadValue<Vector2>());
    }
}
