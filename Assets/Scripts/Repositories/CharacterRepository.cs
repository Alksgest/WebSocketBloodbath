using System;
using Models.Items;
using Models.Player;

namespace Repositories
{
    public static class CharacterRepository
    {
        public static Player GetCharacter()
        {
            var inventory = new PlayerInventory();
            inventory.Inventory.Add(new ItemInInventory
            {
                Index = 0,
                Item = new Sword
                {
                    Id = Guid.NewGuid(),
                    InstanceId = Guid.NewGuid(),
                    Description = "Wtf sword",
                    Name = "Wtf sword",
                    SpritePath = "Sprites/Items/WtfSword",
                },
            });
            inventory.Inventory.Add(new ItemInInventory
            {
                Index = 1,
                Item = new ChestArmor
                {
                    Id = Guid.NewGuid(),
                    InstanceId = Guid.NewGuid(),
                    Description = "Wtf chest",
                    Name = "Wtf chest",
                    SpritePath = "Sprites/Items/wtf-chest",
                },
            });
            
            return new Player
            {
                Id = Guid.NewGuid(),
                // Position = playerTransform.position,
                // Rotation = playerTransform.rotation,
                PlayerStats = new PlayerStats(),
                PlayerInventory = inventory
            };
        }
    }
}