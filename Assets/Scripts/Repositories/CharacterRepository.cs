using System;
using Models;
using Models.Items;
using Models.Player;

namespace Repositories
{
    public class CharacterRepository
    {
        public static Player GetCharacter()
        {
            var inventory = new PlayerInventory();
            inventory.Inventory[0] = new ItemInInventory
            {
                Index = 0,
                Item = new Sword
                {
                    Id = new Guid(),
                    InstanceId = new Guid(),
                    Description = "Wtf sword",
                    Name = "Wtf sword",
                    SpritePath = "Sprites/Items/WtfSword",
                }
            };
            return new Player
            {
                Id = Guid.NewGuid(),
                // Position = playerTransform.position,
                // Rotation = playerTransform.rotation,
                PlayerStats = new PlayerStats(),
                PlayerInventory = new PlayerInventory
                {
                    
                }
            };
        }
    }
}