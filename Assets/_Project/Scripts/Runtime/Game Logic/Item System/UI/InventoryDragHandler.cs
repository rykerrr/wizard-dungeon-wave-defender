using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;
using WizardGame.Utility.Infrastructure.Factories;

namespace WizardGame.Item_System.UI
{
    public class InventoryDragHandler : ItemDragHandler
    {
        [SerializeField] public ItemThrowEvent itemThrowEvent;

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (eventData.hovered.Count == 0)
            {
                // drop item
                TryPhysicallyThrowItem(Vector3.zero);
            }

            // base logic is called beforehand on dapperdino's item system implementation, but do we even need to call it
            // if we want the item destroyed?
            base.OnPointerUp(eventData);
        }

        private void TryPhysicallyThrowItem(Vector3 dropForce)
        {
            if (ReferenceEquals(itemSlotUI, null) ||
                ReferenceEquals((InventoryItem) ItemSlotUI.ReferencedSlotItem, null)) return;

            var inventory = itemSlotUI.Inventory;
            var owner = itemSlotUI.Owner;

            var forward = owner.forward;
            var itemDropLocation = owner.position + forward * 3;
            var force = forward * 0.6f + new Vector3(0f, 1.4f, 0f);
            
            var slot = inventory.ItemContainer[itemSlotUI.SlotIndexOnUI];
            Debug.Log($"Item: {slot.invItem}, Quantity: {slot.Quantity}");
            
            var physItem = PhysicalItemFactory.CreateInstance(itemDropLocation, Quaternion.identity,
                (InventoryItem) itemSlotUI.ReferencedSlotItem);
            inventory.ItemContainer.RemoveAt(ItemSlotUI.SlotIndexOnUI);

            itemThrowEvent.Raise(new ItemThrowData(physItem, force));
        }
    }
}