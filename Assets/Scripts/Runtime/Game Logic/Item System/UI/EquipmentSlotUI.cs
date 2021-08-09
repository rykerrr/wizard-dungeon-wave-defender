using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Item_System.Equipment_system;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    public class EquipmentSlotUI : ItemSlotUI
    {
        [SerializeField] private EquipmentType type;
        [SerializeField] private HotbarItem referencedItem;

        public override HotbarItem ReferencedSlotItem
        {
            get => referencedItem;
            protected set
            {
                referencedItem = value;
                
                UpdateSlotUi();
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
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (dragHandler == null || dragHandler.ItemSlotUI == null) return;

            var newItem = dragHandler.ItemSlotUI.ReferencedSlotItem;
            if (!(newItem is EquippableItem equippable)) return;

            if (type == equippable.EquipmentType)
            {
                if (referencedItem is EquippableItem prevItem) prevItem.UnEquip();
                
                referencedItem = dragHandler.ItemSlotUI.ReferencedSlotItem;
                
                equippable.Equip();
                
                UpdateSlotUi();
            }
        }

        public void ClearItem()
        {
            ((EquippableItem) ReferencedSlotItem).UnEquip();

            ReferencedSlotItem = null;
            
            UpdateSlotUi();
        }
    }
}