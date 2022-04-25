using System;
using System.Collections.Generic;
using Models.Items;
using UnityEngine;

namespace Models.Player
{
    [Serializable]
    public class PlayerInventory
    {
        public const int InventorySize = 5;
        public const int QuickAccessPanelSize = 3;
        [field: SerializeField] public List<ItemInInventory> Inventory { get; }
        [field: SerializeField] public List<ItemInInventory> QuickAccessPanel { get; }

        public PlayerInventory()
        {
            Inventory = new List<ItemInInventory>();
            QuickAccessPanel = new List<ItemInInventory>();
        }
    }
}