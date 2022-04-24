using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Bullet
    {
        [SerializeField] private string id;
        [SerializeField] private string playerId;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string PlayerId
        {
            get => playerId;
            set => playerId = value;
        }
    }
}