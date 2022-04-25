using System;

namespace Models.Items
{
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
}