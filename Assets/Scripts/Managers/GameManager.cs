using System;
using System.Collections.Generic;
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
        public static GameManager Instance;
        
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject otherPlayerPrefab;

        private WebSocket _ws;

        private const string DefaultGameServerUrl = "ws://0.tcp.eu.ngrok.io:14522";

        private Player _mainPlayerModel;
        private GameObject _mainPlayerGo;

        private readonly IDictionary<string, PlayerControllerBase> _playerIdToPlayer =
            new Dictionary<string, PlayerControllerBase>();

        private readonly Queue<string> _gameServerMessageQueue = new();

        private Dictionary<string, Action<string>> _handleServerMessageDictionary;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
            while (_gameServerMessageQueue.Count > 0)
            {
                HandleServerMessage(_gameServerMessageQueue.Dequeue());
            }
        }

        private void OnDestroy()
        {
            CloseSession();
        }

        public void CloseSession()
        {
            _ws.Close(CloseStatusCode.Normal);
        }

        public void SyncPlayerState(GameObject playerGo)
        {
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

        private void InitWebSocketClient()
        {
            var storedUrl = SettingsManager.GameSettings.ServerUrl;
            var url = string.IsNullOrEmpty(storedUrl) ? DefaultGameServerUrl : storedUrl;
            _ws = new WebSocket(url);
            _ws.Connect();
            _ws.OnMessage += QueueServerMessage;
        }

        private void InitMainPlayer()
        {
            var playerPos = new Vector3(0, 1, 0);
            _mainPlayerGo = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            var mainPlayerScript = _mainPlayerGo.GetComponent<PlayerController>();

            var uuid = Guid.NewGuid().ToString();
            
            var playerTransform = transform;
            _mainPlayerModel = new Player
            {
                Id = uuid,
                Position = playerTransform.position,
                Rotation = playerTransform.rotation,
                PlayerStats = new PlayerStats
                {
                    Hits = 0,
                    Hp = 100
                }
            };
            
            mainPlayerScript.Init(_mainPlayerModel);

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
            catch (Exception e)
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

            AddOtherPlayer(playerEnterMessage.Player);
        }

        private void HandlePlayerExitServerMessage(string messageJson)
        {
            var playerExitMessage = JsonConvert.DeserializeObject<ServerMessagePlayerExit>(messageJson);

            if (playerExitMessage == null) return;

            var playerId = playerExitMessage.Player.Id;

            if (!_playerIdToPlayer.ContainsKey(playerId)) return;

            _playerIdToPlayer[playerId].DestroyPlayer(); 
            _playerIdToPlayer.Remove(playerId);
        }

        private void HandlePlayerUpdateServerMessage(string messageJson)
        {
            var playerUpdateMessage = JsonConvert.DeserializeObject<ServerMessagePlayerUpdate>(messageJson);

            if (playerUpdateMessage == null) return;

            var playerModel = playerUpdateMessage.Player;

            if (!_playerIdToPlayer.ContainsKey(playerModel.Id)) return;

            _playerIdToPlayer[playerModel.Id].transform.position = playerModel.Position;
            _playerIdToPlayer[playerModel.Id].transform.rotation = playerModel.Rotation;
        }


        private void HandlePlayerShootMessage(string messageJson)
        {
            var gameStateMessage = JsonConvert.DeserializeObject<ServerMessagePlayerShoot>(messageJson);

            if (gameStateMessage == null) return;

            if (!_playerIdToPlayer.ContainsKey(gameStateMessage.Player.Id)) return;

            var player = _playerIdToPlayer[gameStateMessage.Player.Id];

            if (player is PlayerController) return;

            player.CreateBullet(gameStateMessage);
        }

        private void HandleGameStateServerMessage(string messageJson)
        {
            var gameStateMessage = JsonConvert.DeserializeObject<MessageGameState>(messageJson);

            if (gameStateMessage == null) return;

            foreach (var player in gameStateMessage.GameState.Players)
            {
                // TODO: remove this code after backend changes
                player.PlayerStats ??= new PlayerStats();
                AddOtherPlayer(player);
            }
        }

        private void AddOtherPlayer(Player otherPlayerModel)
        {
            if (otherPlayerModel.Id == _mainPlayerModel.Id ||
                _playerIdToPlayer.ContainsKey(otherPlayerModel.Id)) return;

            var otherPlayerGo = Instantiate(
                otherPlayerPrefab,
                otherPlayerModel.Position,
                Quaternion.identity
            );

            var playerController = otherPlayerGo.GetComponent<PlayerControllerBase>();
            
            playerController.Init(otherPlayerModel);

            _playerIdToPlayer.Add(otherPlayerModel.Id, playerController);
        }
    }
}
