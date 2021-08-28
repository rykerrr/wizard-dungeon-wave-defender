using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Utility.Timers;

namespace WizardGame.Tooltips
{
    public class StatusEffectListingTooltip : TooltipPopupBase
    {
        private StatusEffectListingUI curSelectedListing = default;

        private StringBuilder sb = new StringBuilder();

        private Dictionary<StatusEffectBase, ITimer> timers = new Dictionary<StatusEffectBase, ITimer>();
        
        // Tooltips go inactive in HideTooltip, or more specifically, when they're not used
        // so this isn't really a performance cost as Update only runs when the object is active
        protected override void Update()
        {
            if (curSelectedListing == null) return;

            base.Update();
            UpdateTooltipText();
        }
        
        public void ShowTooltip(StatusEffectListingUI listing)
        {
            SubscribeToGivenListing(listing);
            AddTimersFromCurrentListing();

            prevObj = (RectTransform) curSelectedListing.transform;

            ShowTooltip();
            UpdateTooltipText();
        }
        
        public override void HideTooltip()
        {
            base.HideTooltip();

            timers.Clear();
            UnsubscribeFromCurrentListing();
        }

        private void AddTimersFromCurrentListing()
        {
            foreach (var statEff in curSelectedListing.StatusEffects)
            {
                var timer = TimerTickerSingleton.Instance.GetTimer(statEff);
                
                timers.Add(statEff, timer);
            }
        }

        private void StatusEffectAdded(StatusEffectBase statEff)
        {
            var timer = TimerTickerSingleton.Instance.GetTimer(statEff);
            
            timers.Add(statEff, timer);
        }

        private void StatusEffectRemoved(StatusEffectBase statEff)
        {
            timers.Remove(statEff);
        }
        
        private void UpdateTooltipText()
        {
            sb.Clear();

            sb.Append("Name").Append("\t\tit b").Append("Time").Append("\t\t").Append("Hash code").AppendLine();
            
            foreach (var kvp in timers)
            {
                sb.Append(kvp.Key.Name).Append("\t").Append(kvp.Value.Time).Append("\t")
                    .Append(kvp.Key.GetHashCode()).AppendLine();
            }

            UpdateTooltipText(sb.ToString());
        }

        private void SubscribeToGivenListing(StatusEffectListingUI listing)
        {
            curSelectedListing = listing;
            
            curSelectedListing.onStatusEffectAdded += StatusEffectAdded;
            curSelectedListing.onStatusEffectRemoved += StatusEffectRemoved;
        }

        private void UnsubscribeFromCurrentListing()
        {
            if (curSelectedListing == null) return;
            
            curSelectedListing.onStatusEffectAdded -= StatusEffectAdded;
            curSelectedListing.onStatusEffectRemoved -= StatusEffectRemoved;
            
            curSelectedListing = null;
        }
    }
}