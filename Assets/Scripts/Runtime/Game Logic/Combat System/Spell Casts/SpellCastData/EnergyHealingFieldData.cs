using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class EnergyHealingFieldData : BaseSpellCastData
    {
        [SerializeField] private int tickAmount = 1;
        [SerializeField] private float fieldSize = 1;
        [SerializeField] private float healStrength = 1;

        private int tickHeal = default;
        private bool tickHealIsDirty = true;
        
        public int TickAmount
        {
            get => tickAmount;
            set
            {
                tickAmount = value;
                manaCostIsDirty = true;
                tickHealIsDirty = true;
            }
        }

        public float FieldSize
        {
            get => fieldSize;
            set
            {
                fieldSize = value;
                manaCostIsDirty = true;
                tickHealIsDirty = true;
            }
        }

        public float HealStrength
        {
            get => healStrength;
            set
            {
                healStrength = value;
                manaCostIsDirty = true;
                tickHealIsDirty = true;
            }
        }

        public int TickHeal
        {
            get
            {
                if (tickHealIsDirty)
                {
                    tickHeal = CalculateTickHeal();
                    tickHealIsDirty = false;
                }

                return tickHeal;
            }
        }
        
        public int CalculateTickHeal()
        {
            var retVal = (int) Math.Round(2f / (healStrength + tickAmount) + 3f / fieldSize);

            return retVal;
        }

        protected override int CalculateManaCost()
        {
            var retVal = (int) Math.Round(tickAmount * 3f + healStrength * 10f + fieldSize * 5f);

            return retVal;
        }
    }
}