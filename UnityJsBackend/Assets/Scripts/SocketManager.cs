using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SocketManager : MonoBehaviour
{
    public static SocketManager Instance { get; private set; }
    private readonly string connectionUrl = "http://localhost:1337";
    public static string ConnectionUrl { get{return Instance.connectionUrl;} }

    private Queue<Action> executeOnMainThreadQueue = new Queue<Action>();

    private SocketIOUnity socket;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var uri = new Uri(connectionUrl);
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
                {
                    {"token", "UNITY" }
                }
            ,
            EIO = 4
            ,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        });
        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("socket.OnConnected");
        };
        socket.OnPing += (sender, e) =>
        {
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("disconnect: " + e);
            lock(executeOnMainThreadQueue)
            {
                executeOnMainThreadQueue.Enqueue( () =>
                {
                    GameManager.Instance.OpenAuthScreen();
                });
            }
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{System.DateTime.Now} Reconnecting: attempt = {e}");
        };
        Debug.Log("Connecting...");

        socket.On("startbattle", response => {
            // we are not on the main thread, so enqueue
            lock (executeOnMainThreadQueue)
            {
                executeOnMainThreadQueue.Enqueue(() =>
                {
                    Debug.Log("Start Battle");
                    Debug.Log(response);
                    BattleManager.Instance.StartBattle(response);
                });
            }
        });

        socket.On("turnResult", response => {
            // we are not on the main thread, so enqueue
            lock (executeOnMainThreadQueue)
            {
                executeOnMainThreadQueue.Enqueue(() =>
                {
                    Debug.Log("Next Turn");
                    Debug.Log(response);
                    BattleManager.Instance.StartTurn(response);
                });
            }
        });

        socket.On("battleResult", response => {
            // we are not on the main thread, so enqueue
            lock (executeOnMainThreadQueue)
            {
                executeOnMainThreadQueue.Enqueue(() =>
                {
                    Debug.Log("Battle Ended");
                    Debug.Log(response);
                    BattleManager.Instance.BattleEnded(response);
                });
            }
        });

        socket.Connect();
    }

    private void Update()
    {
        // now we are on the main thread, so execute queue
        while(executeOnMainThreadQueue.Count > 0)
        {
            executeOnMainThreadQueue.Dequeue().Invoke();
        }
    }

    public void SearchBattle(int characterId)
    {
        var searchBattleObj = new JObject();
        searchBattleObj.Add("characterId", characterId);
        searchBattleObj.Add("jwt", GameManager.Instance.JwtToken);

        socket.Emit("searchBattle", searchBattleObj);
    }

    public void SendTurn(int skillSlot) 
    {
        var turnObject = new JObject
        {
            { "jwt", GameManager.Instance.JwtToken },
            { "skillSlot", skillSlot }
        };
        socket.Emit("sendTurn", turnObject);
    }
}
