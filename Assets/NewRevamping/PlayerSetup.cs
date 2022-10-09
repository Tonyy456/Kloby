using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerInputManager manager;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject prefab;

    public void Start()
    {
        /*
         * public void JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, 
         * string controlScheme = null, InputDevice pairWithDevice = null);
         */
        manager.playerPrefab = prefab;
        Game.playerParent = playerParent;
        SetKeyboardController();
        manager.JoinPlayer(playerIndex: 0, pairWithDevice: Keyboard.current);
        manager.JoinPlayer(playerIndex: 1, pairWithDevice: Keyboard.current);
        /*
        switch (Game.Keyboard)
        {
            case 0:
                SetKeyboards();
                break;
            case 1:
                SetKeyboardController();
                break;
            case 2:
                SetControllerController();
                break;
            default:
                SetKeyboards();
                break;
        }
        */

    }
/*
    public void SetKeyboards()
    {
        manager.JoinPlayer(1, controlScheme: "KlobyKeyboard1", pairWithDevice: Keyboard.current);
        manager.JoinPlayer(playerIndex: 1, controlScheme: "KlobyKeyboard2", pairWithDevice: Keyboard.current).ActivateInput();
    }
*/

    public void SetKeyboardController()
    {

    }

    /*
    public void SetControllerController()
    {
        manager.JoinPlayer(0, -1, "KlobyController1", Gamepad.all[0]);
        manager.JoinPlayer(1, -1, "KlobyController2", Gamepad.all[1]);
    }
    */
}
