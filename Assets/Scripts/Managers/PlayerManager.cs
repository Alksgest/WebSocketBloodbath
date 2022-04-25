using System;
using System.Collections.Generic;
using Controllers;
using Controllers.PlayerControllers;
using Models.Player;
using Repositories;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        public static PlayerStatsController PlayerController;
        
        [SerializeField] private GameObject playerPrefab;
        private readonly IDictionary<Guid, PlayerStatsController> _playerIdToPlayer =
            new Dictionary<Guid, PlayerStatsController>();

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            LoadCharacter(true);
        }

        private void LoadCharacter(bool isMainPlayer)
        {
            var playerPos = new Vector3(-4, 0.5f,-8);
            var playerObject = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            var mainPlayerScript = playerObject.GetComponent<PlayerStatsController>();
            
            var player = CharacterRepository.GetCharacter();
            
            mainPlayerScript.Init(player);

            _playerIdToPlayer[player.Id] = mainPlayerScript;

            InitInventory(player);

            if (isMainPlayer)
            {
                PlayerController = mainPlayerScript;
            }
        }

        private void InitInventory(Player player)
        {
            InventoryManager.Instance.InitInventory(player);
        }
    }
}