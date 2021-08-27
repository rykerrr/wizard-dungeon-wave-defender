using System;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class TimedStatBuffData : BaseSpellCastData
    {
        [SerializeField] private StatType statType = default;
        [SerializeField] private float duration = 5f;

        private float buffStrength;
        private bool buffStrengthIsDirty = true;

        public StatType StatType
        {
            get => statType;
            set
            {
                statType = value;
                manaCostIsDirty = true;
                buffStrengthIsDirty = true;
            }
        }

        public float Duration
        {
            get => duration;
            set
            {
                duration = value;
                manaCostIsDirty = true;
                buffStrengthIsDirty = true;
            }
        }

        public float BuffStrength
        {
            get
            {
                if (buffStrengthIsDirty)
                {
                    buffStrength = CalculateBuffStrength();
                    buffStrengthIsDirty = false;
                }

                return buffStrength;
            }
        }
        
        public TimedStatBuffData() { }

        public TimedStatBuffData(TimedStatBuffData data)
        {
            StatType = data.StatType;
            Duration = data.Duration;
        }
        
        private int CalculateBuffStrength()
        {
            var retVal = (int) Math.Round(3f / duration);

            return retVal;
        }
        
        protected override int CalculateManaCost()
        {
            var retVal = (int) Math.Round(5f * duration + 6f * BuffStrength);

            return retVal;
        }
    }
}