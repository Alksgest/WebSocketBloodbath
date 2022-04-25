using System;
using UnityEngine;

namespace Models.Items
{
    [Serializable]
    public class ItemInInventory
    {
        [field: SerializeField] public int Index { get; set; }
        [field: SerializeField] public IItem Item { get; set; }
    }
}