using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    [SerializeField] private CameraFollow camFollower;
    [SerializeField] private GameObject prefab;

    private void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            SetUnityTransport();
            NetworkManager.Singleton.StartServer();
        });
        hostBtn.onClick.AddListener(() =>
        {
            SetUnityTransport();
            NetworkManager.Singleton.StartHost();
            GameObject go = NetworkManager.Instantiate(prefab);
            go.GetComponent<NetworkObject>().SpawnWithOwnership(0);
            camFollower.Register(go);
        });
        clientBtn.onClick.AddListener(() =>
        {
            SetUnityTransport();
            transport.StartClient();
        });
    }

    private UnityTransport transport
    {
        get => NetworkManager.Singleton.GetComponent<UnityTransport>();
    }
    private void SetUnityTransport()
    {
        var comp = NetworkManager.Singleton.GetComponent<UnityTransport>();
        comp.SetConnectionData(
            "127.0.0.1",
            7777
            );
    }
}
