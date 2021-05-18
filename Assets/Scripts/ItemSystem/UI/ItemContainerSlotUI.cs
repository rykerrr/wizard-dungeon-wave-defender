using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    public class ItemContainerSlotUI : ItemSlotUI
    {
        [SerializeField] private Inventory inventory;
        
        public ItemSlot ItemSlot => inventory.ItemContainer.GetSlotByIndex(SlotIndex);
        public override HotbarItem ReferencedSlotItem => ItemSlot.invItem;

        public override void OnDrop(PointerEventData eventData)
        {
            var dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            
            // should technically be impossible, but maybe we'll have more draggable things that aren't items
            if (dragHandler == null) return;

            switch (dragHandler.ItemSlotUI)
            {
                case ItemContainerSlotUI itemSlotUI:
                {
                    inventory.ItemContainer.Swap(itemSlotUI.SlotIndex, SlotIndex);
                    
                    break;
                }
                case HotbarSlotUI hotbarSlotUI:
                {

                    break;
                }
            }
        }
        
        public override void UpdateSlotUi()
        {
            if (ItemSlot.invItem == null)
            {
                EnableSlotUI(false);
            }
            else
            {
                EnableSlotUI(true);

                slotItemIconImage.sprite = ItemSlot.invItem.Icon;
                itemQuantText.text = ItemSlot.Quantity > 0 ? ItemSlot.Quantity.ToString() : " ";
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantText.enabled = enable;
        }
    }
}