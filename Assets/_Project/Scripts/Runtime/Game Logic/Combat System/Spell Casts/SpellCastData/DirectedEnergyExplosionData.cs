using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class DirectedEnergyExplosionData : BaseSpellCastData
    {
        [SerializeField] private int explosionAmount = 1;
        [SerializeField] private float explosionSize = 1;
        [SerializeField] private ExplosionLocationType location = ExplosionLocationType.Self;

        private int baseExplosionDamage = 1;
        private bool damageIsDirty = true;
        
        public enum ExplosionLocationType
        {
            Self = 1,
            Mouse = 2,
            NearestEnemy = 3
        }

        public int ExplosionAmount
        {
            get => explosionAmount;
            set
            {
                explosionAmount = value;
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

        public ExplosionLocationType Location
        {
            get => location;
            set
            {
                location = value;
                manaCostIsDirty = true;
                damageIsDirty = true;
            }
        }

        public int BaseExplosionDamage
        {
            get
            {
                if (damageIsDirty)
                {
                    baseExplosionDamage = CalculateExplosionDamage();
                    damageIsDirty = false;
                }

                return baseExplosionDamage;
            }
        }

        public DirectedEnergyExplosionData() { }

        public DirectedEnergyExplosionData(DirectedEnergyExplosionData data)
        {
            ExplosionAmount = data.ExplosionAmount;
            ExplosionSize = data.ExplosionSize;
            Location = data.Location;
        }
        
        private int CalculateExplosionDamage()
        {
            var retVal = (int)Math.Round(3f / explosionAmount + 10f / explosionSize) / (int)location;

            return retVal;
        }
        
        protected override int CalculateManaCost()
        {
            var manaValue = (int)Math.Round(explosionAmount * 1f + (int)location * 3f + explosionSize * 5f);

            return manaValue;
        }
    }
}