using ItemSystem.World_Interaction;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.Item_Containers
{
    public interface IItemContainer
    {
        bool HasItem(InventoryItem item);
        // interesting thought, GetSlotByIndex could be readonly as it's never supposed to
        // to modify anything within the derived classes
        ItemSlot this[int index] { get; }
        ItemSlot AddItem(ItemSlot itemSlot);
        ItemSlot Remove(ItemSlot itemSlot);
        void RemoveAt(int slotIndex, int quantity);
        int GetTotalQuantity(InventoryItem item);
        void Swap(int slotIndexOne, int slotIndexTwo);
        PhysicalItem DropItem(int slotIndex, Vector3 location);
    }
}