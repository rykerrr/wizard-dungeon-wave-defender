using System.Linq;
using System.Text;
using UnityEngine;
using WizardGame.Stats_System;
using WizardGame.Stats_System.UI;

namespace WizardGame.Tooltips
{
    public class StatTooltipPopup : TooltipPopupBase
    {
        private StringBuilder sb = new StringBuilder();
        
        public void ShowTooltip(StatDisplayUI statDisplayUI)
        {
            if (statDisplayUI == null || statDisplayUI.ThisStat == null) return;

            UpdateTooltipText(statDisplayUI.ThisStat);
            
            prevObj = (RectTransform)statDisplayUI.transform;
            ShowTooltip();
        }

        // When a tooltip is enabled for a given stat, and that stat updates (e.g you level up for whatever reason,
        // enemy dies by DOT, your base stat value changes or you gain a modifier, this gets called to update it
        public void TryUpdateTooltip(StatDisplayUI statDisplayUI)
        {
            var prevObjNullOrDiffObj = ReferenceEquals(prevObj, null) ||
                                       !ReferenceEquals(statDisplayUI.gameObject, prevObj.gameObject);
            
            if (prevObjNullOrDiffObj) return;
            
            UpdateTooltipText(statDisplayUI.ThisStat);            
        }
        
        private void UpdateTooltipText(StatBase statBase)
        {
            var displayStr = StatModifiersToDisplayString(statBase);
            
            UpdateTooltipText(displayStr);
        }

        private string StatModifiersToDisplayString(StatBase statBase)
        {
            var modifiers = statBase.StatModifiers;
            modifiers = modifiers.OrderByDescending(x => x.Source).ToList();
            // sort list by source?
            
            sb.Clear();
            
            sb.Append("Name: ").Append(statBase.Name).AppendLine();
            sb.Append("Actual value: ").Append(statBase.ActualValue).AppendLine();
            sb.Append("Base value: ").Append(statBase.BaseValue).AppendLine();
            sb.Append("Growth rate: ").Append(statBase.GrowthRate).AppendLine();
            
            if(modifiers.Count > 0)
            {
                sb.Append("Modifiers: ").AppendLine();
                sb.Append("Source   Value   Modifier Type");
                sb.AppendLine();
                
                foreach (var mod in modifiers)
                {
                    sb.Append("• ");

                    if (mod.Source == null)
                    {
                        sb.Append("N/A");
                    }
                    else
                    {
                        sb.Append(mod.Source);
                    }

                    sb.Append("     ");
                    
                    sb.Append(mod.Value).Append("     ");
                    sb.Append(mod.Type).Append("     ");

                    sb.AppendLine();
                }
            }
            
            return sb.ToString();
        }
    }
}