using UnityEngine.EventSystems;

namespace WizardGame.Item_System.UI
{
    public class ItemContainerDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            if (eventData.hovered.Count == 0)
            {
                // item was dragged over nothing, destroy it?
                // or perhaps drop?
            }

            // base logic is called beforehand on dapperdino's item system implementation, but do we even need to call it
            // if we want the item destroyed?
            base.OnPointerUp(eventData);
        }
    }
}