using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class EnergyPillarData : BaseSpellCastData
    {
        // Only thing is, buff/shield strength, perhaps it'd depend solely on the int stat?
        [SerializeField] private int shockwaveAmount = 1;
        [SerializeField] private float spellSize = 1; // including the shockwave size 
        [SerializeField] private float delayBetweenWaves = 1;

        private int baseShockwaveDamage = default;
        private bool swDamageIsDirty = true;

        public int ShockwaveAmount
        {
            get => shockwaveAmount;
            set
            {
                shockwaveAmount = value;
                manaCostIsDirty = true;
                swDamageIsDirty = true;
            }
        }

        public float SpellSize
        {
            get => spellSize;
            set
            {
                spellSize = value;
                manaCostIsDirty = true;
                swDamageIsDirty = true;
            }
        }

        public float DelayBetweenWaves
        {
            get => delayBetweenWaves;
            set
            {
                delayBetweenWaves = value;
                manaCostIsDirty = true;
                swDamageIsDirty = true;
            }
        }

        public int BaseShockwaveDamage
        {
            get
            {
                if (swDamageIsDirty)
                {
                    baseShockwaveDamage = CalculateSwDamage();
                    swDamageIsDirty = false;
                }

                return baseShockwaveDamage;
            }
        }
        
        private int CalculateSwDamage()
        {
            var retVal = (int)Math.Round(2f / shockwaveAmount + 1f / spellSize + 5f / delayBetweenWaves);

            return retVal;
        }

        protected override int CalculateManaCost()
        {
            var retVal = (int) Math.Round(4f * shockwaveAmount + 3f * spellSize + 3f * delayBetweenWaves);

            return retVal;
        }
    }
}