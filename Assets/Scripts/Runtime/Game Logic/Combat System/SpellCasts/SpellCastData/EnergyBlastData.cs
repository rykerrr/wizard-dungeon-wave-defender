using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class EnergyBlastData : BaseSpellCastData
    {
        [SerializeField] private int blastAmount = 1;
        [SerializeField] private float spellSize = 1; 
        [SerializeField] private float explosionSize = 1;

        private int baseImpactDamage = 1;
        private int baseExplosionDamage = 1;
        private bool damageIsDirty = true;
        
        public int BlastAmount
        {
            get => blastAmount;
            set
            {
                blastAmount = value;
                manaCostIsDirty = true;
                damageIsDirty = true;
            }
        }

        public float ImpactSize
        {
            get => spellSize;
            set
            {
                spellSize = value;
                manaCostIsDirty = true;
                damageIsDirty = true;
            }
        }

        public float ExplosionSize
        {
            get => explosionSize;
            set
            {
                explosionSize = value;
                manaCostIsDirty = true;
                damageIsDirty = true;
            }
        }

        public int BaseImpactDamage
        {
            get
            {
                if (damageIsDirty)
                {
                    UpdateDamageValues();
                    damageIsDirty = false;
                }

                return baseImpactDamage;
            }
        }

        public int BaseExplosionDamage
        {
            get
            {
                if (damageIsDirty)
                {
                    UpdateDamageValues();
                    damageIsDirty = false;
                }

                return baseExplosionDamage;
            }
        }

        private void UpdateDamageValues()
        {
            baseImpactDamage = (int) Math.Round(10f / spellSize + 5f / BlastAmount);
            baseExplosionDamage = (int) Math.Round(5f / explosionSize + 7f / BlastAmount);
        }
        
        protected override int CalculateManaCost()
        {
            // ball amount equal to 1 mana
            // impact size equal to 5 mana
            // explosion size equal to 3 mana
            // there could be a class that dictates over this

            var manaValue = (int)Math.Round(BlastAmount * 1f + spellSize * 5f + explosionSize * 3f);

            return manaValue;
        }
    }
}