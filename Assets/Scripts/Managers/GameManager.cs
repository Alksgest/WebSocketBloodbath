using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Controllers;
using Models;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketMessages;
using WebSocketSharp;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject otherPlayerPrefab;

        private WebSocket _ws;
        // private const string GameServerUrl = "ws://localhost:5000";

        private const string GameServerUrl = "ws://0.tcp.eu.ngrok.io:14522";

        private Player _mainPlayerModel;
        private GameObject _mainPlayerGo;

        private readonly IDictionary<string, GameObject> _playerIdToOtherPlayerGo =
            new Dictionary<string, GameObject>();

        private readonly Queue<string> _gameServerMessageQueue = new();


        private Dictionary<string, Action<string>> _handleServerMessageDictionary;


        private void Start()
        {
            _handleServerMessageDictionary = new Dictionary<string, Action<string>>
            {
                {ServerMessageType.PlayerEnter, HandlePlayerEnterServerMessage},
                {ServerMessageType.PlayerExit, HandlePlayerExitServerMessage},
                {ServerMessageType.PlayerUpdate, HandlePlayerUpdateServerMessage},
                {ServerMessageType.GameState, HandleGameStateServerMessage},
                {ServerMessageType.PlayerShoot, HandlePlayerShootMessage}
            };
        
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

        public void SyncPlayerState(GameObject playerGo)
        {
            // send "player update" message to server
            _mainPlayerModel.Position = playerGo.transform.position;
            _mainPlayerModel.Rotation = playerGo.transform.rotation;
            var playerUpdateMessage = new ClientMessagePlayerUpdate
            {
                Player = _mainPlayerModel
            };
        
            var json = JsonConvert.SerializeObject(playerUpdateMessage);
            _ws.Send(json);
        }
    
        public void PlayerShoot(Position shootVector, Position shootPosition)
        {
            var playerShootMessage = new ClientMessagePlayerShoot
            {
                Player = _mainPlayerModel,
                ShootVector = shootVector,
                ShootPosition = shootPosition
            };
        
            var json = JsonConvert.SerializeObject(playerShootMessage);
            _ws.Send(json);
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
            var playerPos = new Vector3(0, 1, 0);
            _mainPlayerGo = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            var mainPlayerScript = _mainPlayerGo.GetComponent<PlayerController>();
        
            var uuid = Guid.NewGuid().ToString();

            mainPlayerScript.gameManager = this;
            mainPlayerScript.isMainPlayer = true;
            mainPlayerScript.Init(uuid);
            
            var playerTransform = transform;
            _mainPlayerModel = new Player
            {
                Id = uuid,
                Position = playerTransform.position,
                Rotation = playerTransform.rotation
            };
            
            var playerEnterMessage = new ClientMessagePlayerEnter
            {
                Player = _mainPlayerModel
            };
        
            var json = JsonConvert.SerializeObject(playerEnterMessage);
            _ws.Send(json);
        }

        private void QueueServerMessage(object sender, MessageEventArgs e)
        {
            Debug.Log("Server message received: " + e.Data);
            _gameServerMessageQueue.Enqueue(e.Data);
        }

        private void HandleServerMessage(string messageJson)
        {
            try
            {
                var messageType = JsonConvert.DeserializeObject<MessageBase>(messageJson)?.MessageType;

                if (messageType == null) return;

                if (!_handleServerMessageDictionary.ContainsKey(messageType)) return;

                _handleServerMessageDictionary[messageType](messageJson);
            }
            catch(Exception e)
            {
                Debug.LogWarning("-=-=-=-=-=- Server message handling error -=-=-=-=-=-");
                Debug.LogWarning(e.Message);
                Debug.LogWarning(e.StackTrace);
                Debug.LogWarning("-=-=-=-=-=- Server message handling error -=-=-=-=-=-");

            }
        }

        private void HandlePlayerEnterServerMessage(string messageJson)
        {
            var playerEnterMessage = JsonConvert.DeserializeObject<ServerMessagePlayerEnter>(messageJson);

            if (playerEnterMessage == null) return;
            
            AddOtherPlayerFromPlayerModel(playerEnterMessage.Player);
        }

        private void HandlePlayerExitServerMessage(string messageJson)
        {
            var playerExitMessage = JsonConvert.DeserializeObject<ServerMessagePlayerExit>(messageJson);
            
            if (playerExitMessage == null) return;
            
            var playerId = playerExitMessage.Player.Id;
            
            if (!_playerIdToOtherPlayerGo.ContainsKey(playerId)) return;
            
            Destroy(_playerIdToOtherPlayerGo[playerId]);
            _playerIdToOtherPlayerGo.Remove(playerId);
        }

        private void HandlePlayerUpdateServerMessage(string messageJson)
        {
            var playerUpdateMessage = JsonConvert.DeserializeObject<ServerMessagePlayerUpdate>(messageJson);
            
            if (playerUpdateMessage == null) return;
            
            var playerModel = playerUpdateMessage.Player;
            
            if (!_playerIdToOtherPlayerGo.ContainsKey(playerModel.Id)) return;
            
            _playerIdToOtherPlayerGo[playerModel.Id].transform.position = playerModel.Position;
            _playerIdToOtherPlayerGo[playerModel.Id].transform.rotation = playerModel.Rotation;
        }


        private static void HandlePlayerShootMessage(string messageJson)
        {
            var gameStateMessage = JsonConvert.DeserializeObject<ServerMessagePlayerShoot>(messageJson);

            if (gameStateMessage == null) return;

            //TODO: use _playerIdToOtherPlayerGo
            var players =
                FindObjectsOfType<PlayerController>();

            var player = players.SingleOrDefault(el => el.id == gameStateMessage.Player.Id);

            if (player == null || player.isMainPlayer) return;

            player.CreateBullet(gameStateMessage.ShootPosition, gameStateMessage.ShootVector);
        }

        private void HandleGameStateServerMessage(string messageJson)
        {
            var gameStateMessage = JsonConvert.DeserializeObject<MessageGameState>(messageJson);
            
            if (gameStateMessage == null) return;
            
            foreach (var player in gameStateMessage.GameState.Players)
            {
                AddOtherPlayerFromPlayerModel(player);
            }
        }

        private void AddOtherPlayerFromPlayerModel(ISharedObject otherPlayerModel)
        {
            if (otherPlayerModel.Id == _mainPlayerModel.Id ||
                _playerIdToOtherPlayerGo.ContainsKey(otherPlayerModel.Id)) return;
        
            var otherPlayerGo = Instantiate(
                otherPlayerPrefab,
                otherPlayerModel.Position,
                Quaternion.identity
            );
        
            var otherPlayerScript = otherPlayerGo.GetComponent<PlayerController>();
        
            otherPlayerScript.gameManager = this;
            otherPlayerScript.isMainPlayer = false;
            otherPlayerScript.Init(otherPlayerModel.Id);
        
            _playerIdToOtherPlayerGo.Add(otherPlayerModel.Id, otherPlayerGo);
        }
    }
}
