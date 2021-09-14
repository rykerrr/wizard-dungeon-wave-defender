using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class EnergyBoltData : BaseSpellCastData
    {
        [SerializeField] private float impactSize = 1;
        [SerializeField] private float explosionSize = 1;

        private int baseImpactDamage = default;
        private int baseExplosionDamage = default;
        private bool damageIsDirty = true;
        
        public float ImpactSize
        {
            get => impactSize;
            set
            {
                impactSize = value;
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

        public EnergyBoltData() { }

        public EnergyBoltData(EnergyBoltData data)
        {
            ImpactSize = data.ImpactSize;
            ExplosionSize = data.ExplosionSize;
        }
        
        private void UpdateDamageValues()
        {
            // / 2 for spell type, only makes sense given it's instantaneous
            baseImpactDamage = (int) Math.Round(10f / impactSize + 5f) / 2;
            baseExplosionDamage = (int) Math.Round(5f / explosionSize + 7f) / 2;
        }
        
        
        protected override int CalculateManaCost()
        {
            var manaValue = (int)Math.Round(impactSize * 5f + explosionSize * 3f) * 3; // 3f at end is 
            // for the spell type

            return manaValue;
        }
    }
}