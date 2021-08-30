using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Tooltips;

namespace WizardGame.Item_System.UI
{
    public class ShowItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ItemSlotUI thisSlotUI;
        [SerializeField] private ItemTooltipPopup tooltip;
        
        private void TryShowTooltip() => tooltip.ShowTooltip(thisSlotUI);
        private void TryHideTooltip() => tooltip.HideTooltip();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!thisSlotUI.ReferencedSlotItem || eventData.dragging) return;

            TryShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!thisSlotUI.ReferencedSlotItem) return;

            TryHideTooltip();
        }
    }
}
