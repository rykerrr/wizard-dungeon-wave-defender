using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Tooltips;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class InjectTooltipIntoTooltipShower : MonoBehaviour
    {
        [SerializeField] private StatusEffectListingTooltip tooltip = default;
        
        public void TryInject(StatusEffectListingUI listing)
        {
            var tooltipShower = listing.GetComponent<ShowStatusEffectListingTooltip>();
            tooltipShower.Init(tooltip);
        }
    }
}
