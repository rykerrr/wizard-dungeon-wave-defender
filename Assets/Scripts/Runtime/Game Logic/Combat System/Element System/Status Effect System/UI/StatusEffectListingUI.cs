using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectListingUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI statEffCount = default;
        [SerializeField] private Image icon = default;
        
        private readonly List<StatusEffect> statusEffects = new List<StatusEffect>();

        public void UpdateImageIcon(Sprite sprite)
        {
            icon.sprite = sprite;
        }
        
        public void AddStatusEffect(StatusEffect statusEffect)
        {
            statusEffects.Add(statusEffect);

            var count = statusEffects.Count;

            if (count > 1)
            {
                statEffCount.text = "" + count;
            }
            else
            {
                statEffCount.text = "";
            }
        }

        public bool RemoveStatusEffect(StatusEffect statusEffect)
        {
            statusEffects.Remove(statusEffect);
            
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
                return true;
            }

            return false;
        }
    }
}