using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Tooltips;

namespace WizardGame.Stats_System.UI
{
    public class StatDisplayUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Properties")]
        [SerializeField] private StatType thisStatKey = default;
        
        [Header("References")]
        [SerializeField] private StatTooltipPopup statTooltipPopup = default;
        [SerializeField] private StatsSystemBehaviour statsSysBehav = default;
        [SerializeField] private TextMeshProUGUI statDisplayText = default;

        // rename thisStat to something else?
        private StatsSystem statsSys;
        private StatBase thisStat;

        public StatBase ThisStat => thisStat;

        private void Awake()
        {
            statsSys = statsSysBehav.StatsSystem;
            
            thisStat = statsSys.GetStat(thisStatKey);
            thisStat.statWasModified += OnStatUpdate;

            OnStatUpdate();
        }

        private void OnStatUpdate()
        {
            statDisplayText.text = thisStat.Name + ":   " + thisStat.ActualValue;

            statTooltipPopup.TryUpdateTooltip(this);
            
            // stat : actual value

            // only problem is, we'd pass stat modifiers to ShowTooltip and it'd foreach
            // through them every time

            // if you can figure out a way to cache the stat modifier text properly
            // it might be more performant than that
            // but stringbuilder should work just fine anyway

            // we'll also need an UpdateTooltip method that'll get called from here
            // if the stat updates while we show the tooltip
        }

        private void TryShowTooltip() => statTooltipPopup.ShowTooltip(this);
        private void HideTooltip() => statTooltipPopup.HideTooltip();
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            
            TryShowTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideTooltip();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HideTooltip();
        }
    }
}