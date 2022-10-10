using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerInputManager manager;
    [SerializeField] private GameObject playerParent;
    [SerializeField] private GameObject prefab;

    public void Awake()
    {
        /*
         * public void JoinPlayer(int playerIndex = -1, int splitScreenIndex = -1, 
         * string controlScheme = null, InputDevice pairWithDevice = null);
         */
        Game.playerParent = playerParent;
        
/*        manager.playerPrefab = prefab;
        manager.EnableJoining();
        manager.JoinPlayer();
*/
    }

}
