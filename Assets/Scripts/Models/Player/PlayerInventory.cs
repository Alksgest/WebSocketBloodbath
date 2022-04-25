using System;
using Models.Items;
using UnityEngine;

namespace Models.Player
{
    [Serializable]
    public class PlayerInventory
    {
        public const int InventorySize = 20;
        public const int QuickAccessPanelSize = 10;
        [field: SerializeField] public ItemInInventory[] Inventory { get; }
        [field: SerializeField] public ItemInInventory[] QuickAccessPanel { get; }
        
        public PlayerInventory()
        {
            Inventory = new ItemInInventory[InventorySize];
            QuickAccessPanel = new ItemInInventory[QuickAccessPanelSize];
            
            for (var i = 0; i < InventorySize; ++i)
            {
                Inventory[i] = new ItemInInventory
                {
                    Index = i,
                    Item = null
                };
            }
            
            for (var i = 0; i < QuickAccessPanelSize; ++i)
            {
                QuickAccessPanel[i] = new ItemInInventory
                {
                    Index = i,
                    Item = null
                };
            }
        }
    }
}