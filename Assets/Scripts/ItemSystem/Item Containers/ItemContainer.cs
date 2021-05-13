using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Ludiq;
using UnityEngine;

namespace WizardGame.ItemSystem.Item_Containers
{
    [Serializable]
    public class ItemContainer : IItemContainer
    {
        private ItemSlot[] itemSlots = default;
        
        public Action OnItemsUpdated = delegate { };
        // may be able to convert to custom event system though might not need to as it already works with it
        // OnItemsUpdated gets invoked, inventory SO's subscribed custom event's raise method is invoked and the listeners
        // deal with it, basically ends up being something like chaining for events?
        
        public ItemContainer(int size) => itemSlots = new ItemSlot[size];

        public bool HasItem(InventoryItem item) => itemSlots.Contains(new ItemSlot(item, 0), new ItemComparer());
        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        // if we used ID's, we could pass in an ID here instead
        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            var firstEmptySlotIndex = itemSlots.Length;
            var maxStack = itemSlot.invItem.MaxStack;

            for (var i = itemSlots.Length - 1; i >= 0; i--)
            {
                if (ReferenceEquals(itemSlots[i].invItem, null))
                {
                    firstEmptySlotIndex = Mathf.Min(firstEmptySlotIndex, i);
                    continue;
                }

                if (itemSlots[i].Quantity >= maxStack)
                {
                    firstEmptySlotIndex = Mathf.Min(firstEmptySlotIndex, i);
                    continue;
                }

                if (!ReferenceEquals(itemSlot.invItem, itemSlots[i].invItem))
                {
                    firstEmptySlotIndex = Mathf.Min(firstEmptySlotIndex, i);
                    continue;
                }

                var spaceLeftInSlot = maxStack - itemSlots[i].Quantity;

                if (spaceLeftInSlot <= itemSlot.Quantity)
                {
                    // we add 3 (itemSlot.Quantity), and 2 space is left (slotQuantityLeft), 3 - 2 = 1 item left to add
                    itemSlots[i].Quantity = maxStack;

                    itemSlot.Quantity -= spaceLeftInSlot;

                    // AddItem(itemSlot);

                    continue;
                }

                // slotQuantityLeft is higher or equal to itemSlot quantity, meaning,
                // we have enough space to fit the entire thing in or we have too much space
                itemSlots[i].Quantity += itemSlot.Quantity;

                itemSlot.Quantity = 0;

                return itemSlot;
            }

            for (var i = 0; i < itemSlots.Length; i++)
            {
                if (!ReferenceEquals(itemSlots[i].invItem, null)) continue;
                if (itemSlot.Quantity <= 0) break;

                var newSlotQuant = 0;

                if (itemSlot.Quantity >= maxStack)
                {
                    newSlotQuant = itemSlot.invItem.MaxStack;
                    itemSlot.Quantity -= itemSlot.invItem.MaxStack;
                }
                else
                {
                    newSlotQuant = itemSlot.Quantity;
                    itemSlot.Quantity = 0;
                }

                itemSlots[i] = new ItemSlot(itemSlot.invItem, newSlotQuant);
            }

            return itemSlot;
        }

        public ItemSlot SubtractItem(ItemSlot itemSlot)
        {
            // tries to find existing slot, if it does checks if it can remove quantity and does so if possible
            // otherwise removes the item and continues searching
            // this method might be much better implemented differently
            for (var i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].invItem != itemSlot.invItem) continue;

                if (itemSlot.Quantity > itemSlots[i].Quantity)
                {
                    itemSlot.Quantity -= itemSlots[i].Quantity;
                    itemSlots[i].Quantity = 0;
                }
                else
                {
                    itemSlots[i].Quantity -= itemSlot.Quantity;
                    itemSlot.Quantity = 0;

                    break;
                }
            }

            return itemSlot;
        }

        public void RemoveAt(int slotIndex)
        {
            ItemSlot slot = itemSlots[slotIndex];

            if (slot.invItem == null) return;

            itemSlots[slotIndex] = new ItemSlot(slot.invItem, 0);
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            int totalQuant = 0;

            for (int i = itemSlots.Length - 1; i >= 0; i--)
            {
                if (itemSlots[i].invItem == item)
                {
                    totalQuant += itemSlots[i].Quantity;
                }
            }

            return totalQuant;
        }

        public void Swap(int slotIndexOne, int slotIndexTwo)
        {
            // if they are the same exact slot, return
            // if the items are the same, check whether you can stack items from slotOne onto slotTwo
            // if you can't, swap them
            // if the items are not the same, swap them

            var sameSlot = slotIndexOne == slotIndexTwo;
            if (sameSlot) return;

            var slotOne = itemSlots[slotIndexOne];
            var slotTwo = itemSlots[slotIndexTwo];

            if (ReferenceEquals(slotOne.invItem, slotTwo.invItem))
            {
                var itemMaxStack = slotOne.invItem.MaxStack;
                var slotTwoIsNotFull = slotTwo.Quantity < itemMaxStack;

                if (slotTwoIsNotFull)
                {
                    var spaceLeftInSlotTwo = itemMaxStack - slotTwo.Quantity;
                    
                    itemSlots[slotIndexTwo].Quantity += spaceLeftInSlotTwo;
                    itemSlots[slotIndexOne].Quantity -= spaceLeftInSlotTwo;

                    return;
                }
            }
            
            itemSlots[slotIndexTwo] = slotOne;
            itemSlots[slotIndexOne] = slotTwo;
        }
    }
}