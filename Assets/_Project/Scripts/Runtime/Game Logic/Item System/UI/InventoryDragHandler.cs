using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.World_Interaction;

namespace WizardGame.Item_System.UI
{
    public class InventoryDragHandler : ItemDragHandler
    {
        private ItemThrower itemThrower = default;

        protected override void Awake()
        {
            base.Awake();

            // Debug.Log(itemSlotUI);
            // Debug.Log(itemSlotUI.ReferencedSlotItem);
            ((InventorySlotUI) itemSlotUI).LogItemSlot();

            itemThrower = new ItemThrower((InventorySlotUI) itemSlotUI);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (eventData.hovered.Count == 0)
            {
                itemThrower.TryPhysicallyThrowItem(Vector3.zero);
            }

            // base logic is called beforehand on dapperdino's item system implementation, but do we even need to call it
            // if we want the item destroyed?
            base.OnPointerUp(eventData);
        }
    }
}