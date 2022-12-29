using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController2DTD : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private Transform ballPrefab;
    private Transform go;

    private NetworkVariable<MyData> randomNumber = new NetworkVariable<MyData>(new MyData {
        myId = 0,
        isCool = false,
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct MyData : INetworkSerializable
    {
        public int myId;
        public bool isCool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref myId);
            serializer.SerializeValue(ref isCool);
            serializer.SerializeValue(ref message);
        }
    }

    [ClientRpc]
    public void TestClientRpc(ClientRpcParams item)
    {
        Debug.Log($"Received message from {OwnerClientId}");
    }

    [ServerRpc]
    public void TestServerRpc()
    {
        Debug.Log("TestServerRpc " + OwnerClientId);
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyData previousValue, MyData newValue) =>
        {        
            //Debug.Log(OwnerClientId + $" values: {newValue.myId},{newValue.isCool},{newValue.message}");
        };
        base.OnNetworkSpawn();
    }
    public void Update()
    {   
        if (!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.T))
        {
            Transform obj = Instantiate(ballPrefab);
            obj.GetComponent<NetworkObject>().Spawn(true);
            /*
            TestClientRpc(new ClientRpcParams() { Send = new ClientRpcSendParams() { TargetClientIds = new List<ulong>() { 1 } } });
            randomNumber.Value = new MyData()
            {
                myId = Random.Range(0,100),
                isCool = !randomNumber.Value.isCool,
                message = "test data",
            };
            */
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            Destroy(go.gameObject);
        }

        Vector3 keyDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) keyDirection.y = +1f;
        if (Input.GetKey(KeyCode.A)) keyDirection.x = -1f;
        if (Input.GetKey(KeyCode.S)) keyDirection.y = -1f;
        if (Input.GetKey(KeyCode.D)) keyDirection.x = +1f;

        transform.position += keyDirection * moveSpeed * Time.deltaTime;
        if (keyDirection.magnitude > 0.1f)
        {
            Quaternion quaternionRotation = Quaternion.LookRotation(this.transform.forward, keyDirection);
            transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternionRotation, 360f);
        }
    }
}
