using System;
using UnityEngine;

namespace Models.Items
{
    public enum ItemType : byte
    {
        None = 0,
        Chest,
        Boots,
        Legs,
        Helmet,
        Weapon
    }
    
    public interface IItem
    {
        public Guid Id { get; }
        public Guid InstanceId { get; }
        public string Name { get; }
        public float Price { get; }
        public float Weight { get; }
        public float Durability { get; }
        public ItemType ItemType { get; }
        public string SpritePath { get; }
        public string ModelPath { get; }
        public string Description { get; }
    }
    
    [Serializable]
    public class Sword : IItem
    {
        [field: SerializeField] public Guid Id { get; set; }
        [field: SerializeField] public Guid InstanceId { get; set; }
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public float Price { get; set; }
        [field: SerializeField] public float Weight { get; set; }
        [field: SerializeField] public float Capacity { get; set; }
        [field: SerializeField] public float CurrentVolume { get; set; }
        [field: SerializeField] public float Durability { get; set; }
        [field: SerializeField] public string SpritePath { get; set; }
        [field: SerializeField] public string ModelPath { get; set; }
        [field: SerializeField] public ItemType ItemType { get; } = ItemType.Weapon;
        [field: SerializeField] public string Description { get; set; }
    }
}