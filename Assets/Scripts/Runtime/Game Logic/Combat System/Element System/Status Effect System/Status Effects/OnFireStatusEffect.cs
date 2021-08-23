using System;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class OnFireStatusEffect : StatusEffect
    {
        private int tickDamage;
        
        private HealthSystem targetHealthSys;

        public int TickDamage => tickDamage;

        public override void Init(GameObject caster, GameObject target, StatusEffectData data)
        {
            base.Init(caster, target, data);

            tickDamage = data.DamagePerFrame;
            
            targetHealthSys = target.GetComponent<HealthSystemBehaviour>().HealthSystem;
        }

        public override void Tick()
        {
             // Smack this on a timer?
             // Add some particle emission on the timer too?
            
             targetHealthSys.TakeDamage(tickDamage);
        }

        public override void OnRemove()
        {
            
        }
    }
}