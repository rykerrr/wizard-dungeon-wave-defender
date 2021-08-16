using System;
using System.Collections.Generic;
using System.Reflection;
using Ludiq;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectHandler : MonoBehaviour
    {
        [Header("Current status effect data")]
        [SerializeField] private List<BaseStatusEffect> currentStatusEffects = new List<BaseStatusEffect>();

        public IReadOnlyCollection<BaseStatusEffect> CurrentStatusEffects => currentStatusEffects.AsReadOnly();
        
        private void Update()
        {
            for (int i = currentStatusEffects.Count - 1; i >= 0; i--)
            {
                Debug.Log(currentStatusEffects[i]);
                currentStatusEffects[i].Tick();
            }
        }

        private void AddRemovalTimerForStatus(BaseStatusEffect statusEffect, float duration)
        {
            var timer = new DownTimer(duration)
            {
                OnTimerEnd = () =>
                {
                    statusEffect.OnRemove();
                    RemoveStatusEffect(statusEffect);
                }
            };
            
            TimerTickerSingleton.Instance.AddTimer(timer);
        }

        // target will always be the current gameobject
        public bool AddStatusEffect(BaseStatusEffect statusEffect, float duration)
        {
            if (statusEffect == null)
            {
                Debug.Log("This is null?");
                return false;
            }
            
            if (currentStatusEffects.Contains(statusEffect)) return false;
            
            currentStatusEffects.Add(statusEffect);
            
            AddRemovalTimerForStatus(statusEffect, duration);
            
            return true;
        }

        // Called ForceRemove as potions and spells would technically FORCE the removal of the status effects
        // So would status effects canceling each other out such as water and fire canceling each other out
        public bool ForceRemoveStatusEffect(BaseStatusEffect statusEffect)
        {
            return RemoveStatusEffect(statusEffect);
        }
        
        private bool RemoveStatusEffect(BaseStatusEffect statusEffect) => currentStatusEffects.Remove(statusEffect);

        [Header("Debug data")]
        [SerializeField] private ElementStatusEffectData debugStatEffData = default;
        
        [ContextMenu("Add given status effect")]
        public void AddStatusGivenEffect()
        {
            var statEff = debugStatEffData.StatusEffect;
            
            var thisGoDebug = gameObject;
            statEff.Init(thisGoDebug, thisGoDebug, debugStatEffData);

            AddStatusEffect(statEff, debugStatEffData.Duration);
        }

        [ContextMenu("Remove given status effect")]
        public void RemoveStatusGivenEffect()
        {
            var statEff = debugStatEffData.StatusEffect;
            
            ForceRemoveStatusEffect(statEff);
        }
    }
}