using System.Text;
using UnityEngine;
using WizardGame.Item_System.Items;
using WizardGame.Item_System.UI;

namespace WizardGame.Tooltips
{
    public class ItemTooltipPopup : TooltipPopupBase
    {
        private StringBuilder sb = new StringBuilder();
        
        public void ShowTooltip(ItemSlotUI itemSlotUI)
        {
            if (itemSlotUI == null || itemSlotUI.ReferencedSlotItem == null) return;

            UpdateTooltipText(itemSlotUI.ReferencedSlotItem);
            
            prevObj = (RectTransform)itemSlotUI.transform;
            ShowTooltip();
        }

        private void UpdateTooltipText(HotBarItem item)
        {
            sb.Append("<size=35>").Append(item.ColouredName).Append("</size>").AppendLine();
            sb.Append(item.GetInfoDisplayText());

            UpdateTooltipText(sb.ToString());
            sb.Clear();
        }
    }
}