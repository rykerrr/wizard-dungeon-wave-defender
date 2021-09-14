using System;
using UnityEngine;

namespace WizardGame.Stats_System
{
    [Serializable]
    public class EntityStat
    {
        [SerializeField] private string name = default;
        [SerializeField] private StatType statType = default;
        [SerializeField] private float growthRate = default;

        public StatType StatType => statType;
        public float GrowthRate => growthRate;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public EntityStat(StatType statType, float growthRate, string name)
        {
            this.statType = statType;
            this.growthRate = growthRate;
            this.name = name;
        }

        public override string ToString()
        {
            return $"Entity Stat | Type: {statType} | GrowthRate: {growthRate}";
        }
    }
}