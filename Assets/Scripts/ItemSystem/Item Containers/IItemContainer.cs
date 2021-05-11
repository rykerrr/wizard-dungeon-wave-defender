namespace WizardGame.ItemSystem.Item_Containers
{
    public interface IItemContainer
    {
        bool HasItem(InventoryItem item);
        // interesting thought, GetSlotByIndex could be readonly as it's never supposed to
        // to modify anything within the derived classes
        ItemSlot GetSlotByIndex(int index);
        ItemSlot AddItem(ItemSlot itemSlot);
        ItemSlot SubtractItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        int GetTotalQuantity(InventoryItem item);
        void Swap(int slotIndexOne, int slotIndexTwo);
    }
}