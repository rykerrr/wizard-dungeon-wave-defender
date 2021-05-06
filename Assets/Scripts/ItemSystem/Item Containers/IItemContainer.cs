namespace WizardGame.ItemSystem.Item_Containers
{
    public interface IItemContainer
    {
        bool HasItem(InventoryItem item);
        ItemSlot GetSlotByIndex(int index);
        ItemSlot AddItem(ItemSlot itemSlot);
        ItemSlot SubtractItem(ItemSlot itemSlot);
        void RemoveAt(int slotIndex);
        int GetTotalQuantity(InventoryItem item);
        void Swap(int slotIndexOne, int slotIndexTwo);
    }
}