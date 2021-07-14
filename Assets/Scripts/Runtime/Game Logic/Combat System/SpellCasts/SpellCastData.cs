using System;
using System.Text;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class SpellCastData
    {
        [Header("Properties")]
        [SerializeField] private float castingSpeed = 1;
        [SerializeField] private float castCooldownMultiplier = 1;
        [SerializeField] private float speedMultiplier = 1;
        [SerializeField] private float spellStrength = 1;
        
        [SerializeField] private float gravMagnitudeMultiplier = 1f;
        
        private float gravMagnitude = default;
        private bool gravIsDirty = true;

        private StringBuilder sb = new StringBuilder();

        public float GravMagnitude
        {
            get
            {
                if (gravIsDirty)
                {
                    gravMagnitude = CalculateGrav();
                    gravIsDirty = false;
                }

                return gravMagnitude;
            }
        }

        public SpellCastData(float castingSpeed, float castCooldownMultiplier, float speedMultiplier,
            float spellStrength) : this()
        {
            this.speedMultiplier = speedMultiplier;
            this.spellStrength = spellStrength;
            this.castingSpeed = castingSpeed;
            this.castCooldownMultiplier = castCooldownMultiplier;
        }

        public SpellCastData() => sb = new StringBuilder();

        public float CastingSpeed
        {
            get => castingSpeed;
            set
            {
                castingSpeed = value;
            }
        }

        public float CastCooldownMultiplier
        {
            get => castCooldownMultiplier;
            set
            {
                castCooldownMultiplier = value;
            }
        }

        public float SpeedMultiplier
        {
            get => speedMultiplier;
            set
            {
                speedMultiplier = value;
                gravIsDirty = true;
            }
        }

        public float SpellStrength
        {
            get => spellStrength;
            set
            {
                spellStrength = value;
                gravIsDirty = true;
            }
        }
        
        public float CalculateGrav()
        {
            return speedMultiplier * spellStrength / ((speedMultiplier + spellStrength)
            * gravMagnitudeMultiplier);
        }

        public override string ToString()
        {
            base.ToString();

            // Can't figure out why StringBuilder isn't being initialized in the field, I assume it's due to the 
            // unity's serialization, but if you don't initialize it here in one way or another it'll throw
            // a null ref exception
            // (sb ??= new StringBuilder()).Clear();

            sb.Append("Spell Cast Data | Cast Speed Multiplier: ").Append(CastingSpeed)
                .Append(", Cast Cooldown Multiplier:").Append(CastCooldownMultiplier)
                .Append(", Projectile Speed Multiplier: ").Append(SpeedMultiplier)
                .Append(", Spell Strength Multiplier: ").Append(SpellStrength)
                .Append(", Gravity Magnitude: ").Append(GravMagnitude).AppendLine();
            
            return sb.ToString();
        }
    }
}