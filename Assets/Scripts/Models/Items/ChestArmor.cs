using System;
using UnityEngine;

namespace Models.Items
{
    [Serializable]
    public class ChestArmor : IItem
    {
        [field: SerializeField] public Guid Id { get; set; }
        [field: SerializeField] public Guid InstanceId { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public float Price { get; set; }
        [field: SerializeField] public float Weight { get; set; }
        [field: SerializeField] public float Durability { get; set; }
        [field: SerializeField] public string SpritePath { get; set; }
        [field: SerializeField] public string ModelPath { get; set; }
        [field: SerializeField] public ItemType ItemType { get; } = ItemType.Weapon;
        [field: SerializeField] public string Description { get; set; }
    }
}