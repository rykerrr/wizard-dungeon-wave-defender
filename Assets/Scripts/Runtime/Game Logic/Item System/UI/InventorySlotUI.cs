using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    public class InventorySlotUI : ItemSlotUI
    {
        public override HotbarItem ReferencedSlotItem => ItemSlot.invItem;
        
        private ItemSlot ItemSlot => inventory.ItemContainer[SlotIndexOnUI];

        public override void OnDrop(PointerEventData eventData)
        {
            // Explanation for my sanity
            // As expected, this gets called on the object that has an ItemIcon dropped on to them
            // We get the drag handler to see what got dropped on us
            // If it was an itemcontainerslotui, we swap it, if it was a hotbarslotui we do nothing
            // You can try doing that on your own
            
            var dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
            
            if (dragHandler == null) return;

            switch (dragHandler.ItemSlotUI)
            {
                case InventorySlotUI itemSlotUI:
                {
                    inventory.ItemContainer.Swap(itemSlotUI.SlotIndexOnUI, SlotIndexOnUI);
                    cdDisplay.UpdateData(ReferencedSlotItem);
                    
                    break;
                }
                case EquipmentSlotUI equipSlotUi:
                case HotbarSlotUI hotbarSlotUI:
                {

                    break;
                }
            } 
        }
        
        public override void UpdateSlotUi()
        {
            if (ItemSlot.invItem == null || ItemSlot.Quantity == 0)
            {
                EnableSlotUI(false);
                cdDisplay.UpdateData(null);
            }
            else
            {
                EnableSlotUI(true);
                cdDisplay.UpdateData(ReferencedSlotItem);
                
                slotItemIconImage.sprite = ItemSlot.invItem.Icon;
                
                Debug.Log(ItemSlot.Quantity + "  | " + ItemSlot.invItem);
                itemQuantText.text = ItemSlot.Quantity > 1 ? ItemSlot.Quantity.ToString() : " ";
                Debug.Log(itemQuantText.text);
            }
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantText.enabled = enable;
        }

        [ContextMenu("Log ItemSlot")]
        public void LogItemSlot()
        {
            Debug.Log(ItemSlot + "\n" + ReferencedSlotItem, this);
        }
    }
}