using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Movement.Position;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private List<MovementModifierBehaviour> movements = new List<MovementModifierBehaviour>();
        
        [Header("Current status effect data")]
        // serializing above would do wonders for debugging

        private Dictionary<StatusEffectData, List<StatusEffect>> currentStatusEffects = 
            new Dictionary<StatusEffectData, List<StatusEffect>>();
        
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

        public StatusEffectInteraction GetBuffingInteraction(StatusEffectData statEffType)
        {
            var interactions = statEffType.Interactions;
            if (interactions.Count == 0) return null;

            foreach (var interaction in interactions)
            {
                if (interaction.InteractionType != InteractionType.ModifySpellDamage) continue;
                
                if (currentStatusEffects.ContainsKey(interaction.Target)) return interaction;
            }

            return null;
        }
        
        public StatusEffectAddResult AddStatusEffect(StatusEffectData statEffType, StatusEffect statEff, float duration
        , out StatusEffectInteraction buffInteraction)
        {
            buffInteraction = null;
            
            if (statEff == null || statEffType == null)
            {
                Debug.Log("This is null?");
                return StatusEffectAddResult.Failed;
            }
            
            var res = CheckInteractions(statEffType, out var buff);
            
            // Failed in this case means that there were either no interactions or none
            // with the status effects in here, so we can proceed to add as normal
            if (res == StatusEffectAddResult.SpellBuff || res == StatusEffectAddResult.Finished)
            {
                buffInteraction = buff;
                
                return res;
            }

            var statEffExists = currentStatusEffects.ContainsKey(statEffType);
            
            if (statEffExists)
            {
                // it exists, check if we can stack it, and how
                
                switch (statEff.StackType)
                {
                    case StatusEffectStackType.IgnoreIfExists:
                    {
                        Debug.Log("Status effect exists with IgnoreIfExists, ignoring");
                        Debug.Log("-------------------------------");
                        
                        break;
                    }
                    case StatusEffectStackType.DurationExtend:
                    {
                        // No point in grabbing anything but the first element as it can't stack in the first place
                        
                        var statEffToExt = currentStatusEffects[statEffType][0];
                        
                        Debug.Log("Extending duration, prev duration: "
                                  + TimerTickerSingleton.Instance.GetTimer(statEffToExt).Time);
                        
                        if (!ExtendStatusEffectDuration(duration, statEffToExt))
                        {
                            // Duration wasn't extended for whatever reason, most likely an error
                            return StatusEffectAddResult.Failed;
                        }
                        
                        Debug.Log("New duration: " 
                                  + TimerTickerSingleton.Instance.GetTimer(statEffToExt).Time);
                        Debug.Log("-------------------------------");
                        
                        break;
                    }
                    case StatusEffectStackType.FullStack:
                    {
                        Debug.Log("Full stack, prev stack count: " + currentStatusEffects[statEffType].Count);
                        
                        currentStatusEffects[statEffType].Add(statEff);
                        AddRemovalTimerForStatus(statEffType, statEff, duration);
                        
                        Debug.Log("New stack count: " + currentStatusEffects[statEffType].Count);
                        Debug.Log("-------------------------------");
                        
                        break;
                    }
                    case StatusEffectStackType.DurationRefresh:
                    {
                        // No point in grabbing anything but the first element as it can't stack in the first place

                        var statEffToRef = currentStatusEffects[statEffType][0];
                        
                        Debug.Log("Refreshing duration, prev duration: " + 
                                  TimerTickerSingleton.Instance.GetTimer(statEffToRef).Time);
                        
                        if (!RefreshStatusEffectDuration(statEffToRef))
                        {
                            // Duration wasn't extended for whatever reason, most likely an error
                            return StatusEffectAddResult.Failed;
                        }
                        
                        Debug.Log("New duration: " 
                                  + TimerTickerSingleton.Instance.GetTimer(statEffToRef).Time);
                        Debug.Log("-------------------------------");
                        
                        break;
                    }
                    case StatusEffectStackType.DurationRefreshAndFullStack:
                    {
                        var statEffs = currentStatusEffects[statEffType];
                        
                        Debug.Log("Refreshing duration and full stack, prev count:"
                                  + statEffs.Count + " , Prev dur of first element: " 
                                  + TimerTickerSingleton.Instance.GetTimer(statEffs[0]).Time);
                        
                        foreach (var statEffToRef in statEffs)
                        {
                            if (!RefreshStatusEffectDuration(statEffToRef))
                            {
                                // Duration wasn't extended for whatever reason, most likely an error
                                return StatusEffectAddResult.Failed;
                            }
                        }

                        statEffs.Add(statEff);
                        AddRemovalTimerForStatus(statEffType, statEff, duration);

                        Debug.Log("New count: " + statEffs.Count + " , New dur of first element:"
                        + TimerTickerSingleton.Instance.GetTimer(statEffs[0]).Time);
                        Debug.Log("-------------------------------");
                        
                        break;
                    }
                }
            }
            else
            {
                // it doesn't exist, so we can add it regardless of it's stack type
                
                Debug.Log("Added new status effect: " + statEffType.Name);
                Debug.Log("-------------------------------");
                
                currentStatusEffects.Add(statEffType, new List<StatusEffect>() { statEff });
                AddRemovalTimerForStatus(statEffType, statEff, duration);
            }

            UpdateExternalMovementValue();
            return StatusEffectAddResult.Finished;
        }

        // Runs to check for special interactions, if there are none proceed to add effects normally
        private StatusEffectAddResult CheckInteractions(StatusEffectData data, 
            out StatusEffectInteraction buffInteraction)
        {
            buffInteraction = null;
            
            if (data.Interactions.Count <= 0) return StatusEffectAddResult.Failed;
            
            foreach (var interaction in data.Interactions)
            {
                if (!currentStatusEffects.ContainsKey(interaction.Target)) continue;

                switch (interaction.InteractionType)
                {
                    case InteractionType.RemoveAndCombine:
                    {
                        Debug.Log("Combining, interaction:" + interaction.Name + ", Base: " + data.Name
                                  + " Target: " + interaction.Target.Name + " Result: " + interaction.Result);
                        
                        // remove target, don't add this, add result
                        RemoveAllOfType(interaction.Target);
                        AddStatusEffect(interaction.Result,
                            StatusEffectFactory.CreateStatusEffect(interaction.Result)
                            , interaction.Result.Duration, out var buff);

                        return StatusEffectAddResult.Finished;
                    }
                    case InteractionType.ModifySpellDamage:
                    {
                        // remove target, don't add this, get StatusEffectAddResult.SpellBuff returned
                        // somehow
                        
                        Debug.Log("Buffing spell, interaction:" + interaction.Name + " , Base: " + data.Name
                                  + " , Target: " + interaction.Target.Name 
                                  + " , Effectiveness: " + interaction.Effectiveness);
                        
                        RemoveAllOfType(interaction.Target);
                        buffInteraction = interaction;

                        return StatusEffectAddResult.SpellBuff;
                    }
                    case InteractionType.RemoveBoth:
                    {
                        // remove target, don't add this

                        Debug.Log("Removing both, interaction:" + interaction.Name + " , Base: " + data.Name
                                  + " , Target: " + interaction.Target.Name + " , Result: " + interaction.Result);
                        
                        RemoveAllOfType(interaction.Target);

                        return StatusEffectAddResult.Finished;
                    }
                }
            }

            return StatusEffectAddResult.Failed;
        }
        
        private void AddRemovalTimerForStatus(StatusEffectData statusEffectType, StatusEffect statusEffect, float duration)
        {
            var timer = new DownTimer(duration)
            {
                OnTimerEnd = () =>
                {
                    statusEffect.OnRemove();
                    RemoveStatusEffect(statusEffectType, statusEffect);
                }
            };

            timer.OnTimerEnd += () => Debug.Log("Removed: " + statusEffect + " : " + TimerTickerSingleton.Instance.RemoveTimer(statusEffect));
            
            TimerTickerSingleton.Instance.AddTimer(timer, statusEffect);
        }

        private bool RefreshStatusEffectDuration(StatusEffect statEff)
        {
            var statusEffTimer = (DownTimer) TimerTickerSingleton.Instance
                .GetTimer(statEff);

            if (statusEffTimer == null)
            {
                Debug.LogError("We have a problem chief");
                Debug.Break();

                return false;
            }
            
            statusEffTimer.Reset();

            return true;
        }
        
        
        private bool ExtendStatusEffectDuration(float duration, StatusEffect statEff)
        {
            var statusEffTimer = (DownTimer) TimerTickerSingleton.Instance
                .GetTimer(statEff);

            if (statusEffTimer == null)
            {
                Debug.LogError("We have a problem chief");
                Debug.Break();

                return false;
            }

            statusEffTimer.SetNewTime(statusEffTimer.Time + duration);
            
            return true;
        }

        private void UpdateExternalMovementValue()
        {
            // Reverse the loops to avoid out of range exceptions due to array shifting
            
            float mult = 1;
            
            foreach (var statEff in currentStatusEffects)
            {
                mult *= statEff.Value[0].MovementMultiplier;
            }
            
            foreach (var mv in movements)
            {
                mv.ExternalMult = mult;
            }
        }

        // Called ForceRemove as potions and spells would technically FORCE the removal of the status effects
        // So would status effects canceling each other out such as water and fire canceling each other out
        public void ForceRemoveStatusEffect(StatusEffectData statEffType, StatusEffect statEff)
        {
            RemoveStatusEffect(statEffType, statEff);
        }

        public bool RemoveAllOfType(StatusEffectData statEffType)
        {
            var res = currentStatusEffects.Remove(statEffType);
            UpdateExternalMovementValue();

            return res;
        }

        private void RemoveStatusEffect(StatusEffectData key, StatusEffect statusEffect)
        {
            if (currentStatusEffects.ContainsKey(key))
            {
                if (currentStatusEffects[key].Count == 1)
                {
                    currentStatusEffects.Remove(key);
                }
                else currentStatusEffects[key].Remove(statusEffect);
            }

            UpdateExternalMovementValue();
        }

        #region debug
        [Header("Debug data")]
        [SerializeField] private StatusEffectData debugStatEffData = default;
        [SerializeField] private Element elementOfStatEff = default;
        [SerializeField] private List<StatusEffect> debugPrevAddedStatusEffects = new List<StatusEffect>();
        
        [ContextMenu("Add given status effect")]
        public void AddStatusGivenEffect()
        {
            var statEff = StatusEffectFactory.CreateStatusEffect(debugStatEffData);
            statEff.Init(gameObject, gameObject, elementOfStatEff, debugStatEffData);

            var entryExists = debugPrevAddedStatusEffects.Contains(statEff);
            if (entryExists && (statEff.StackType == StatusEffectStackType.FullStack ||
                statEff.StackType == StatusEffectStackType.DurationRefreshAndFullStack))
            {
                debugPrevAddedStatusEffects.Add(statEff);
            }
            else if (!entryExists)
            {
                debugPrevAddedStatusEffects.Add(statEff);
            }
            // Should also be removed from here once it gets removed by the timer
            
            var res = AddStatusEffect(debugStatEffData, statEff, debugStatEffData.Duration, out var buff);
            Debug.Log(res);
        }

        [ContextMenu("Remove given status effect")]
        public void RemoveStatusGivenEffect()
        {
            if (debugPrevAddedStatusEffects.Count == 0) return;
            
            ForceRemoveStatusEffect(debugStatEffData, debugPrevAddedStatusEffects[0]);
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
        
        [ContextMenu("Dump timers for previously added status effects")]
        private void DumpTimerDataForStatusEffects()
        {
            foreach (var entry in debugPrevAddedStatusEffects)
            {
                Debug.Log(TimerTickerSingleton.Instance.GetTimer(entry));
            }
        }
        #endregion
    }
}