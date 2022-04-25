using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Player
    {
        [field: SerializeField] public string Id { get; set; }
        [field: SerializeField] public PlayerStats PlayerStats { get; set; }
        [field: SerializeField] public Position Position { get; set; }
        [field: SerializeField] public Rotation Rotation { get; set; }
    }
}