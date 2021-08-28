using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Tooltips;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class ShowStatusEffectListingTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private StatusEffectListingUI thisListing = default;

        private StatusEffectListingTooltip tooltip = default;

        public void Init(StatusEffectListingTooltip tooltip)
        {
            this.tooltip = tooltip;
        }

        private void TryShowTooltip() => tooltip.ShowTooltip(thisListing);
        private void TryHideTooltip() => tooltip.HideTooltip();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            TryShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TryHideTooltip();
        }

        private void OnDisable()
        {
            TryHideTooltip();
        }
    }
}