using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectListingUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI statEffCount = default;
        [SerializeField] private Image icon = default;

        public event Action<StatusEffectBase> onStatusEffectAdded = delegate { };
        public event Action<StatusEffectBase> onStatusEffectRemoved = delegate { };
        
        
        private readonly List<StatusEffectBase> statusEffects = new List<StatusEffectBase>();

        public IReadOnlyList<StatusEffectBase> StatusEffects => statusEffects.AsReadOnly();

        public void UpdateImageIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
        
        public void AddStatusEffect(StatusEffectBase statEff)
        {
            statusEffects.Add(statEff);

            var count = statusEffects.Count;

            if (count > 1)
            {
                statEffCount.text = "" + count;
            }
            else
            {
                statEffCount.text = "";
            }
            
            onStatusEffectAdded?.Invoke(statEff);
        }

        public bool RemoveStatusEffect(StatusEffectBase statEff)
        {
            statusEffects.Remove(statEff);
            
            var count = statusEffects.Count;

            if (count > 1)
            {
                statEffCount.text = "" + count;
            }
            else if(count == 1)
            {
                statEffCount.text = "";
            }
            else
            {
                onStatusEffectRemoved?.Invoke(statEff);
                
                return true;
            }

            onStatusEffectRemoved?.Invoke(statEff);
            
            return false;
        }
    }
}