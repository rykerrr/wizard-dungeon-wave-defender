using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    // if OnDrop doesn't get called, implement IDropHandler
    // kind of a test as to whether you can derive from classes and override interface methods without re-implementing
    // the interface itself
    public class HotbarSlotUI : ItemSlotUI
    {
        // Should this not be a struct isolated from the unity UI code?
        [SerializeField] private Inventory inventory = default;

        private HotbarItem referencedSlotSlotItem;
        
        public override HotbarItem ReferencedSlotItem
        {
            get => referencedSlotSlotItem;
            set { referencedSlotSlotItem = value; UpdateSlotUi(); }
        }
        
        public override void OnDrop(PointerEventData eventData)
        {
            var dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (dragHandler == null || dragHandler.ItemSlotUI == null) return;

            switch (dragHandler.ItemSlotUI)
            {
                case HotbarSlotUI hotbarSlotUi:
                {
                    // is this needed? why not allow the item to be referenced on multiple slots?
                    HotbarItem oldItem = ReferencedSlotItem;
                    ReferencedSlotItem = hotbarSlotUi.ReferencedSlotItem;
                    hotbarSlotUi.ReferencedSlotItem = oldItem;
                    // essentially switches them but yea

                    break;
                }
                case ItemContainerSlotUI itemContainerSlotUi:
                {
                    ReferencedSlotItem = itemContainerSlotUi.ReferencedSlotItem;
                    
                    break;
                }
            }
        }

        public bool AddItem(HotbarItem itemToAdd)
        {
            if (!ReferenceEquals(ReferencedSlotItem, null)) return false;
            
            ReferencedSlotItem = itemToAdd;

            return true;
        }

        public void UseItem()
        {
            if (ReferenceEquals(referencedSlotSlotItem, null)) return;
            
            Debug.Log(referencedSlotSlotItem.GetInfoDisplayText());
            
            
        }
        
        private void UpdateItemQuantityUI()
        {
            // what if we have a spell?
            // what if we have a quantity of 0?
            // what if we have a weapon?
            // we have no clue how many potions the inventory actually has, we need to check whether it still has this
            // ...function should be renamed?

            switch (ReferencedSlotItem)
            {
                case InventoryItem invItem:
                {
                    if (inventory.ItemContainer.HasItem(invItem))
                    {
                        int quantCount = inventory.ItemContainer.GetTotalQuantity(invItem);
                        itemQuantText.text = quantCount > 1 ? quantCount.ToString() : "";
                    }
                    else
                    {
                        ReferencedSlotItem = null;
                    }

                    break;
                }
                default:
                {
                    itemQuantText.enabled = false;

                    break;
                }
            }
        }
        
        public override void UpdateSlotUi()
        {
            if (ReferencedSlotItem == null)
            {
                EnableSlotUI(false);
                return;
            }

            slotItemIconImage.sprite = ReferencedSlotItem.Icon;
            EnableSlotUI(true);
            UpdateItemQuantityUI();
            
            // update cooldown UI
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantText.enabled = enable;
        }
    }
}