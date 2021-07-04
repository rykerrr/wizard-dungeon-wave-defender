using System;
using System.Linq;
using System.Text;
using UnityEngine;
using Utilities.Serializable_Data_Types;
using WizardGame.Timers;

namespace WizardGame.Stats_System
{
    [Serializable]
    public class StatsSystem
    {
        [SerializeField]
        private SerializableDictionary<StatType, StatBase> stats = new SerializableDictionary<StatType, StatBase>();
        private SerializableDictionary<StatType, StatBase> Stats => stats;
        
        private StringBuilder sb = new StringBuilder();
        private TimerTickerSingleton timeTicker = default;

        public StatsSystem(EntityStats entityToLoad, TimerTickerSingleton timeTicker)
        {
            this.timeTicker = timeTicker;
            
            Init(entityToLoad);
        }

        public void Init(EntityStats entityToLoad)
        {
            stats.Clear();

            InitializeStats(entityToLoad);
        }

        private void InitializeStats(EntityStats entityToLoad)
        {
            var defStatList = entityToLoad.DefaultStatTypes;
            var firstDepIndex = entityToLoad.FirstDepStatIndex;

            for (int i = 0; i < firstDepIndex; i++)
            {
                stats.Add(defStatList[i], new Stat(defStatList[i]));
            }

            for (int i = firstDepIndex; i < defStatList.Count; i++)
            {
                Debug.Log(defStatList[i] + " | " + defStatList[firstDepIndex]);
                var depStatBase = (DependantStatType) defStatList[i];
                var depStat = CreateDependantStat(depStatBase);

                stats.Add(depStatBase, depStat);
            }
        }

        private DependantStat CreateDependantStat(DependantStatType depStatBase)
        {
            DependantStat depStat = new DependantStat(depStatBase);

            foreach (var statDepType in depStatBase.StatsDependingOn)
            {
                if (stats.ContainsKey(statDepType))
                {
                    depStat.AddStatDependency(stats[statDepType]);
                }
                else
                {
                    // But it could be a DependantStat too...
                    StatBase statDependency = new Stat(depStatBase);

                    stats.Add(statDepType, statDependency);
                    depStat.AddStatDependency(statDependency);
                }
            }

            return depStat;
        }

        private StatBase CreateStat(StatType statType)
        {
            StatBase retStat = default;

            switch (statType)
            {
                case BaseStatType baseType:
                {
                    retStat = new Stat(baseType);
                    break;
                }
                case DependantStatType dependantType:
                {
                    retStat = CreateDependantStat(dependantType);
                    break;
                }
            }

            return retStat;
        }

        public StatBase GetStat(StatType statType)
        {
            if (!Stats.ContainsKey(statType))
            {
                #if UNITY_EDITOR
                                Debug.LogWarning("StatType doesn't exist in the stats system. " +
                                                 "Perhaps it's not on the Entity or there has been a problem with " +
                                                 "loading the stats system?");
                #endif

                return null;
            }
            
            return Stats?[statType];
        }
        
        public void AddModifierTo(StatType statType, StatModifier modifierToAdd)
        {
            stats?[statType].AddModifier(modifierToAdd);
        }

        public void AddTimedModifier(StatType statType, StatModifier statModifier, float time)
        {
            DownTimer newTimer = new DownTimer(time);
        
            timeTicker.Timers.Add(newTimer);
            AddModifierTo(statType, statModifier);
        
            newTimer.OnTimerEnd += () => RemoveModifierFrom(statType, statModifier);
            newTimer.OnTimerEnd += () => timeTicker.RemoveTimer(newTimer);
        } 
        
        public bool RemoveModifierFrom(StatType statType, StatModifier modifierToRemove)
        {
            return stats.ContainsKey(statType) && stats[statType].RemoveModifier(modifierToRemove);
        }

        public int RemoveModifierFromSource(StatType statType, object source)
        {
            return stats.ContainsKey(statType) ? stats[statType].RemoveModifiersFromSource(source) : 0;
        }

        public override string ToString()
        {
            if (ReferenceEquals(sb, null)) sb = new StringBuilder();

            foreach (var stat in stats)
            {
                sb.Append($"{stat.Key.Name} : {stat.Value.ActualValue.ToString()} | {stat.Value}").AppendLine();
            }

            var retStr = sb.ToString();
            sb.Clear();

            return retStr;
        }

#if UNITY_EDITOR
        public int CheckModifierOccurrences(StatModifier modifier)
        {
            int occurrences = default;

            foreach (var stat in stats)
            {
                occurrences += stat.Value.Modifiers.Count(x => ReferenceEquals(x, modifier));
            }

            return occurrences;
        }
#endif
    }
}