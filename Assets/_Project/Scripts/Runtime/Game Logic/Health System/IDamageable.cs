using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Health_System
{
    public interface IDamageable
    {
        public DamageResult TakeDamage(int dmg, Element damageElement, GameObject damageSource = null);
    }
}