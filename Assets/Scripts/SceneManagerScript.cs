using System.Collections.Generic;
using Constants;
using UnityEngine;
using WebSocketMessages;
using WebSocketSharp;

public class SceneManagerScript : MonoBehaviour
{

    public GameObject playerPrefab;

    private WebSocket _ws;
    private const string GameServerUrl = "ws://localhost:5000";

    // private const string GameServerUrl = "ws://5.tcp.eu.ngrok.io:15226";

    private Player _mainPlayerModel;
    private GameObject _mainPlayerGo;

    private readonly IDictionary<string, GameObject> _playerIdToOtherPlayerGo =
            new Dictionary<string, GameObject>();

    private readonly Queue<string> _gameServerMessageQueue = new();


    private void Start()
    {
        InitWebSocketClient();
        InitMainPlayer();
    }

    private void Update()
    {
        // process all queued server messages
        while (_gameServerMessageQueue.Count > 0)
        {
            HandleServerMessage(_gameServerMessageQueue.Dequeue());
        }
    }

    private void OnDestroy()
    {
        // close websocket connection
        _ws.Close(CloseStatusCode.Normal);
    }

    // INTERFACE METHODS

    public void SyncPlayerState(GameObject playerGo)
    {
        // send "player update" message to server
        _mainPlayerModel.Position = playerGo.transform.position;
        var playerUpdateMessage = new ClientMessagePlayerUpdate
        {
            Player = _mainPlayerModel
        };
        _ws.Send(JsonUtility.ToJson(playerUpdateMessage));
    }

    // IMPLEMENTATION METHODS

    private void InitWebSocketClient()
    {
        _ws = new WebSocket(GameServerUrl);
        _ws.Connect();
        _ws.OnMessage += QueueServerMessage;
    }

    private void InitMainPlayer()
    {
        // create player game object
        var playerPos = new Vector3(0, 1, 0);
        _mainPlayerGo = Instantiate(playerPrefab, playerPos, Quaternion.identity);
        var mainPlayerScript = _mainPlayerGo.GetComponent<PlayerScript>();
        mainPlayerScript.sceneManager = this;
        mainPlayerScript.isMainPlayer = true;
        // create player model
        var uuid = System.Guid.NewGuid().ToString();
        _mainPlayerModel = new Player
        {
            Id = uuid,
            Position = transform.position
        };
        // send "player enter" message to server
        var playerEnterMessage = new ClientMessagePlayerEnter
        {
            Player = _mainPlayerModel
        };
        _ws.Send(JsonUtility.ToJson(playerEnterMessage));
    }

    private void QueueServerMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Server message received: " + e.Data);
        _gameServerMessageQueue.Enqueue(e.Data);
    }

    private void HandleServerMessage(string messageJson)
    {
        // parse message type
        var messageType = JsonUtility.FromJson<ServerMessageGeneric>(messageJson)?.MessageType;
        // route message to handler based on message type
        if (messageType == ServerMessageType.PlayerEnter)
        {
            HandlePlayerEnterServerMessage(messageJson);
        }
        else if (messageType == ServerMessageType.PlayerExit)
        {
            HandlePlayerExitServerMessage(messageJson);
        }
        else if (messageType == ServerMessageType.PlayerUpdate)
        {
            HandlePlayerUpdateServerMessage(messageJson);
        }
        else if (messageType == ServerMessageType.GameState)
        {
            HandleGameStateServerMessage(messageJson);
        }
    }

    private void HandlePlayerEnterServerMessage(string messageJson)
    {
        var playerEnterMessage = JsonUtility.FromJson<ServerMessagePlayerEnter>(messageJson);
        AddOtherPlayerFromPlayerModel(playerEnterMessage.Player);
    }

    private void HandlePlayerExitServerMessage(string messageJson)
    {
        var playerExitMessage = JsonUtility.FromJson<ServerMessagePlayerExit>(messageJson);
        var playerId = playerExitMessage.Player.Id;
        if (_playerIdToOtherPlayerGo.ContainsKey(playerId))
        {
            Destroy(_playerIdToOtherPlayerGo[playerId]);
            _playerIdToOtherPlayerGo.Remove(playerId);
        }
    }

    private void HandlePlayerUpdateServerMessage(string messageJson)
    {
        var playerUpdateMessage = JsonUtility.FromJson<ServerMessagePlayerUpdate>(messageJson);
        var playerModel = playerUpdateMessage.Player;
        if (_playerIdToOtherPlayerGo.ContainsKey(playerModel.Id))
        {
            var newPosition = new Vector3(
                playerModel.Position.x,
                playerModel.Position.y,
                0
            );
            _playerIdToOtherPlayerGo[playerModel.Id].transform.position = newPosition;
        }
    }

    private void HandleGameStateServerMessage(string messageJson)
    {
        var gameStateMessage = JsonUtility.FromJson<ServerMessageGameState>(messageJson);
        foreach (var player in gameStateMessage.GameState.Players)
        {
            AddOtherPlayerFromPlayerModel(player);
        }
    }

    private void AddOtherPlayerFromPlayerModel(Player otherPlayerModel)
    {
        // player is not main player and player is not currently tracked
        if (
            otherPlayerModel.Id != _mainPlayerModel.Id
            && !_playerIdToOtherPlayerGo.ContainsKey(otherPlayerModel.Id)
        )
        {
            var otherPlayerPosition = new Vector3(
                otherPlayerModel.Position.x,
                otherPlayerModel.Position.y,
                0
            );
            var otherPlayerGo = Instantiate(
                playerPrefab,
                otherPlayerPosition,
                Quaternion.identity
            );
            var otherPlayerScript = otherPlayerGo.GetComponent<PlayerScript>();
            otherPlayerScript.sceneManager = this;
            otherPlayerScript.isMainPlayer = false;
            _playerIdToOtherPlayerGo.Add(otherPlayerModel.Id, otherPlayerGo);
        }
    }
}
