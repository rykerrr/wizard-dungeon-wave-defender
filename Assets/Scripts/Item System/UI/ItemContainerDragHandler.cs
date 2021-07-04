using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;
using WizardGame.Utility.Infrastructure.Factories;

namespace WizardGame.Item_System.UI
{
    public class ItemContainerDragHandler : ItemDragHandler
    {
        [SerializeField] public ItemThrowEvent itemThrowEvent;
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (eventData.hovered.Count == 0)
            {
                // drop item
                TryThrowItem(Vector3.zero);
            }

            // base logic is called beforehand on dapperdino's item system implementation, but do we even need to call it
            // if we want the item destroyed?
            base.OnPointerUp(eventData);
        }

        private void TryThrowItem(Vector3 dropForce)
        {
            if (ReferenceEquals(itemSlotUI, null) || 
                ReferenceEquals((InventoryItem)ItemSlotUI.ReferencedSlotItem, null)) return;
            
            Inventory inventory = itemSlotUI.Inventory;
            Transform owner = itemSlotUI.Owner;
            
            Vector3 itemDropLocation = owner.position + owner.forward * 3;
            Vector3 force = owner.forward * 4f + new Vector3(0f, 5f, 0f);
            
            var physItem = PhysicalItemFactory.CreateInstance(itemDropLocation, Quaternion.identity, (InventoryItem)itemSlotUI.ReferencedSlotItem);
            inventory.ItemContainer.RemoveAt(ItemSlotUI.SlotIndexOnUI);
            
            itemThrowEvent.Raise(new ItemThrowData(physItem, force));
        }
    }
}