using UnityEngine;
using WizardGame.Item_System.Items;
using WizardGame.Movement.Position;
using WizardGame.Item_System.UI;

namespace WizardGame.Item_System.World_Interaction
{
    public class ItemThrower
    {
        private ItemSlotUI slotUi = default;
        
        public ItemThrower(ItemSlotUI slotUi)
        {
            this.slotUi = slotUi;
        }
        
        public void TryPhysicallyThrowItem(Vector3 dropForce)
        {
            if (ReferenceEquals(slotUi, null) ||
                ReferenceEquals((InventoryItem) slotUi.ReferencedSlotItem, null)) return;
            
            // remove item
            var inventory = slotUi.Inventory;
            inventory.ItemContainer.RemoveAt(slotUi.SlotIndexOnUI);

            // get throw force
            var owner = slotUi.Owner;
            var forward = owner.forward;
            var force = forward * 0.6f + new Vector3(0f, 1.4f, 0f);

            // get spawn pos
            var itemDropLocation = owner.position + forward * 3;

            // create the item
            var physItem = PhysicalItemFactory.CreateInstance(itemDropLocation, Quaternion.identity,
                (InventoryItem) slotUi.ReferencedSlotItem);

            // add force
            var forceReceiver = physItem.GetComponent<ForceReceiverMovementBehaviour>();
            forceReceiver.AddForce(force);
        }
    }
}