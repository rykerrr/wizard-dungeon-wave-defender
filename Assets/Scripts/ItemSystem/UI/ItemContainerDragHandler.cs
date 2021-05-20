using ItemSystem.World_Interaction;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    public class ItemContainerDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (eventData.hovered.Count == 0)
            {
                // drop item
                TryDropItem();
            }

            // base logic is called beforehand on dapperdino's item system implementation, but do we even need to call it
            // if we want the item destroyed?
            base.OnPointerUp(eventData);
        }

        private void TryDropItem()
        {
            ItemContainerSlotUI itemContainerSlotUI = (itemSlotUI as ItemContainerSlotUI);
            if (ReferenceEquals(itemContainerSlotUI, null)) return;

            Inventory inventory = itemContainerSlotUI.Inventory;
            Transform owner = itemContainerSlotUI.Owner;

            Vector3 itemDropLocation = owner.position + owner.forward * 3;

            PhysicalItem droppedItem =
                inventory.ItemContainer.DropItem(itemSlotUI.SlotIndexOnUI, itemDropLocation);
            
            Debug.Log(droppedItem, this);
        }
    }
}