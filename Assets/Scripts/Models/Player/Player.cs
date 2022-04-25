using System;
using UnityEngine;

namespace Models.Player
{
    [Serializable]
    public class Player
    {
        [field: SerializeField] public Guid Id { get; set; }
        [field: SerializeField] public PlayerStats PlayerStats { get; set; }
        [field: SerializeField] public PlayerInventory PlayerInventory { get; set; }
        [field: SerializeField] public Position Position { get; set; }
        [field: SerializeField] public Rotation Rotation { get; set; }
    }
}