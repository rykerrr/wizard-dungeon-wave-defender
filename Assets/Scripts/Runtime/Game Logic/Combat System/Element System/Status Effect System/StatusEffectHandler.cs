using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Ludiq;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectHandler : MonoBehaviour
    {
        [Header("Current status effect data")]
        // serializing above would do wonders for debugging

        private Dictionary<Type, List<StatusEffect>> currentStatusEffects = new Dictionary<Type, List<StatusEffect>>();
        // get stat types, aka keys
        // get status effects for type, aka key
        
        private void Update()
        {
            foreach (var statusEffectList in currentStatusEffects)
            {
                foreach (var statusEffect in statusEffectList.Value)
                {
                    statusEffect.Tick();
                }
            }
        }

        private void AddRemovalTimerForStatus(StatusEffect statusEffect, float duration)
        {
            var timer = new DownTimer(duration)
            {
                OnTimerEnd = () =>
                {
                    statusEffect.OnRemove();
                    RemoveStatusEffect(statusEffect);
                }
            };
            timer.OnTimerEnd += () => TimerTickerSingleton.Instance.RemoveTimer(statusEffect);

            TimerTickerSingleton.Instance.AddTimer(timer, statusEffect);
        }

        // returns bool as to whether it was added or not
        public bool AddStatusEffect(StatusEffect statusEffectToAdd, float duration)
        {
            if (statusEffectToAdd == null)
            {
                Debug.Log("This is null?");
                return false;
            }

            var statusType = statusEffectToAdd.GetType();
            var statEffExists = currentStatusEffects.ContainsKey(statusType);
            
            if (statEffExists)
            {
                // it exists, check if we can stack it, and how
                
                switch (statusEffectToAdd.StackType)
                {
                    case StatusEffectStackType.IgnoreIfExists:
                    {
                        return true;
                    }
                    case StatusEffectStackType.DurationExtend:
                    {
                        var statusEffTimer = (DownTimer) TimerTickerSingleton.Instance
                            .GetTimer(statusEffectToAdd);

                        if (statusEffTimer == null)
                        {
                            Debug.LogError("We have a problem chief");
                            Debug.Break();

                            return false;
                        }
                        
                        statusEffTimer.SetNewDefaultTime(statusEffTimer.Time + duration);
                        
                        break;
                    }
                    case StatusEffectStackType.FullStack:
                    {
                        currentStatusEffects[statusType].Add(statusEffectToAdd);
                        AddRemovalTimerForStatus(statusEffectToAdd, duration);
                        
                        break;
                    }
                }
            }
            else
            {
                // it doesn't exist, so we can add it regardless of it's stack type
                
                currentStatusEffects.Add(statusType, new List<StatusEffect>() { statusEffectToAdd });
                AddRemovalTimerForStatus(statusEffectToAdd, duration);
            }

            return true;
        }

        // Called ForceRemove as potions and spells would technically FORCE the removal of the status effects
        // So would status effects canceling each other out such as water and fire canceling each other out
        public void ForceRemoveStatusEffect(StatusEffect statusEffect)
        {
            RemoveStatusEffect(statusEffect);
        }

        public bool RemoveAllOfType(Type statusEffectType)
        {
            return currentStatusEffects.Remove(statusEffectType);
        }

        private void RemoveStatusEffect(StatusEffect statusEffect)
        {
            var statusType = statusEffect.GetType();
            
            if (currentStatusEffects.ContainsKey(statusType))
            {
                currentStatusEffects[statusType].Remove(statusEffect);
            }
        }

        [Header("Debug data")]
        [SerializeField] private StatusEffectData debugStatEffData = default;
        [SerializeField] private List<StatusEffect> debugPrevAddedStatusEffects = new List<StatusEffect>();
        
        [ContextMenu("Add given status effect")]
        public void AddStatusGivenEffect()
        {
            var statEff = StatusEffectFactory.CreateStatusEffect(debugStatEffData);
            statEff.Init(gameObject, gameObject, debugStatEffData);
            
            debugPrevAddedStatusEffects.Add(statEff);
            
            AddStatusEffect(statEff, debugStatEffData.Duration);
        }

        [ContextMenu("Remove given status effect")]
        public void RemoveStatusGivenEffect()
        {
            if (debugPrevAddedStatusEffects.Count == 0) return;
            
            ForceRemoveStatusEffect(debugPrevAddedStatusEffects[0]);
        }

        [ContextMenu("Dump current data")]
        public void DumpStatusEffectData()
        {
            foreach (var kvp in currentStatusEffects)
            {
                Debug.Log(kvp.Key + " | " + kvp.Value[0].GetType() + " | " + kvp.Value[0]
                + "\n" + kvp.Value.Count);
            }
        }

        [ContextMenu("Previously added debug status effects")]
        private void DumpPrevAddedStatusEffects() 
        {
            foreach (var entry in debugPrevAddedStatusEffects)
            {
                Debug.Log(entry);
            }
        }
    }
}