using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Player
    {
        [SerializeField] private string id;
        [SerializeField] private PlayerStats playerStats;
        [SerializeField] private Position position;
        [SerializeField] private Rotation rotation;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public PlayerStats PlayerStats
        {
            get => playerStats;
            set => playerStats = value;
        }

        public Position Position
        {
            get => position;
            set => position = value;
        }

        public Rotation Rotation
        {
            get => rotation;
            set => rotation = value;
        }
    }
}