using System;
using System.Collections.Generic;
using System.Linq;
using Controllers.UI.Items;
using Models.Items;
using Models.Player;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Managers
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        [SerializeField] private GameObject backpack;
        [SerializeField] private GameObject quickAccess;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static bool MoveItem(
            EquipmentSlotController oldSlot,
            EquipmentSlotController newSlot, 
            EquipmentItemController itemController)
        {
            if (newSlot.SlotFilled) return false;

            oldSlot.Clear();

            itemController.currentSlot = newSlot;
            newSlot.currentItem = itemController;
            newSlot.AdjustContent();

            var index = newSlot.transform.parent.GetSiblingIndex();
            var item = itemController.Item;

            var oldTag = oldSlot.tag;
            var newTag = newSlot.tag;

            RemoveItemByTag(oldTag, item);
            AddItemByTag(newTag, item, index);
            
            return true;
        }
        
        private static void AddItemByTag(string slotTag, IItem item, int index)
        {
            
            Debug.Log(index);
            var inventory = PlayerManager.PlayerController.Player.PlayerInventory;
            switch (slotTag)
            {
                case "BackpackSlot":
                    inventory.Inventory.Add(new ItemInInventory
                    {
                        Index = index,
                        Item = item
                    });
                    break;
                case "QuickAccessSlot":
                    inventory.QuickAccessPanel.Add(new ItemInInventory
                    {
                        Index = index,
                        Item = item
                    });
                    break;
            }
        }

        private static void RemoveItemByTag(string slotTag, IItem item)
        {
            var inventory = PlayerManager.PlayerController.Player.PlayerInventory;
            switch (slotTag)
            {
                case "BackpackSlot":
                    var itemInBp = inventory.Inventory.Single(el => el.Item.InstanceId == item.InstanceId);
                    inventory.Inventory.Remove(itemInBp);
                    break;
                case "QuickAccessSlot":
                    var itemInQa = inventory.QuickAccessPanel.Single(el => el.Item.InstanceId == item.InstanceId);
                    inventory.QuickAccessPanel.Remove(itemInQa);
                    break;
            }
        }
        
        public void InitInventory(Player player)
        {
            var bp = player.PlayerInventory.Inventory;
            var qa = player.PlayerInventory.QuickAccessPanel;

            FillInventoryUIPart(player, bp, backpack);
            FillInventoryUIPart(player, qa, quickAccess);
        }

        private static void FillInventoryUIPart(
            Player player, 
            IReadOnlyCollection<ItemInInventory> bp, 
            GameObject go)
        {
            var bpSlots = go.transform.GetChildren();
            var countOfBp = Mathf.Min(bp.Count, bpSlots.Length);

            for (var i = 0; i < countOfBp; ++i)
            {
                var slotController = bpSlots[i].GetChild(0).gameObject.GetComponent<EquipmentSlotController>();
                if (slotController == null)
                {
                    continue;
                }

                var item = bp.Single(el => el.Index == i).Item;
                var itemController = CreateItem(player, item);
                itemController.transform.parent = slotController.transform;

                itemController.currentSlot = slotController;
                slotController.currentItem = itemController;

                slotController.AdjustContent();
            }
        }

        private static EquipmentItemController CreateItem(Player character, IItem item)
        {
            var go = new GameObject();
            go.AddComponent<EquipmentItemController>();
            go.AddComponent<Image>();

            var controller = go.GetComponent<EquipmentItemController>();
            controller.Instantiate(item);

            var image = go.GetComponent<Image>();
            var sprite = Resources.Load<Sprite>(item.SpritePath);
            image.sprite = sprite;

            go.name = $"{item.ItemType}_{item.Name}_{go.GetInstanceID()}";

            return controller;
        }
    }
}