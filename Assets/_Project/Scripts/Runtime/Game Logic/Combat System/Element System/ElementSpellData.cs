using System;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System
{
    [Serializable]
    public class ElementSpellData
    {
        [SerializeField] private float impactStrengthMult = 1f;
        [SerializeField] private float explosionStrengthMult = 1f;
        [SerializeField] private float explosionRadiusMult = 1f;
        [SerializeField] private float healStrengthMult = 1f;
        [SerializeField] private float buffStrengthMult = 1f;
        [SerializeField] private float travelSpeedMult = 1f;

        public float ImpactStrengthMult => impactStrengthMult;
        public float ExplosionStrengthMult => explosionStrengthMult;
        public float ExplosionRadiusMult => explosionRadiusMult;
        public float HealStrengthMult => healStrengthMult;
        public float BuffStrengthMult => buffStrengthMult;
        public float TravelSpeedMult => travelSpeedMult;
    }
}