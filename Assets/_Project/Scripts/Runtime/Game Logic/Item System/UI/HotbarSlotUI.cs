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
        [SerializeField] private HotbarItem referencedSlotSlotItem = default;

        public override HotbarItem ReferencedSlotItem
        {
            get => referencedSlotSlotItem;
            protected set
            {
                referencedSlotSlotItem = value;
                
                UpdateSlotUi();
                
                cdDisplay.UpdateData(referencedSlotSlotItem.CooldownData);
            }
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
                case EquipmentSlotUI equipmentSlotUI:
                {
                    ReferencedSlotItem = equipmentSlotUI.ReferencedSlotItem;

                    break;
                }
                case InventorySlotUI itemContainerSlotUi:
                {
                    ReferencedSlotItem = itemContainerSlotUi.ReferencedSlotItem;

                    break;
                }
            }
        }

        public bool AddItem(HotbarItem itemToAdd)
        {
            Debug.Log("Is this getting called?");

            if (!ReferenceEquals(ReferencedSlotItem, null)) return false;

            ReferencedSlotItem = itemToAdd;

            cdDisplay.UpdateData(ReferencedSlotItem.CooldownData);

            return true;
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
                        var quantCount = inventory.ItemContainer.GetTotalQuantity(invItem);
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
        }

        public void ClearItem()
        {
            ReferencedSlotItem = null;
            
            UpdateSlotUi();
        }
        

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantText.enabled = enable;
        }
    }
}