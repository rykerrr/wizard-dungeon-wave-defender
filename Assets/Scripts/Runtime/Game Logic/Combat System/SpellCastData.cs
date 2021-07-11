using System;
using UnityEngine;

namespace WizardGame.Combat_System
{
    [Serializable]
    public class SpellCastData
    {
        [SerializeField] private float castingTime = default;
        [SerializeField] private float castCooldownDelay = default;

        // add a field for cast circle prefab?

        public SpellCastData(float castingTime, float castCooldownDelay)
        {
            this.castingTime = castingTime;
            this.castCooldownDelay = castCooldownDelay;
        }
        
        public float CastingTime => castingTime;
        public float CastCooldownDelay => castCooldownDelay;
    }
}