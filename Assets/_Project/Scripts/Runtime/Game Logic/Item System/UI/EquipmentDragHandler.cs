using UnityEngine;
using UnityEngine.EventSystems;

namespace WizardGame.Item_System.UI
{
    public class EquipmentDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            // if we drag it off the equipment window, we just want to clear the reference
            // meaning if we drag it on another that isn't a equipmentslotui we want to clear the reference
            base.OnPointerUp(eventData);
            
            if (eventData.hovered.Count != 0) return;

            (ItemSlotUI as EquipmentSlotUI)?.ClearItem();
        }
    }
}