﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace WizardGame.Item_System.UI
{
    public class HotbarDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            // if we drag it off the hotbar, we just want to clear the reference
            // meaning if we drag it on another that isn't a hotbarslotui we want to clear the reference

            if (eventData.button != PointerEventData.InputButton.Left) return;

            base.OnPointerUp(eventData);

            if (eventData.hovered.Count != 0) return;
            
            (ItemSlotUI as HotbarSlotUI).ReferencedSlotItem = null;
        }
    }
}