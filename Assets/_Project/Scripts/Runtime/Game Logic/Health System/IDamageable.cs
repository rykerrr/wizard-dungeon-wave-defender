using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Health_System
{
    public interface IDamageable
    {
        public void TakeDamage(int dmg, Element damageElement, GameObject damageSource = null);
    }
}