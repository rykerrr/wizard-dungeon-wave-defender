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
            
            Debug.Log(caster + " | " + target);
            
            targetHealthSys = target.GetComponent<HealthSystemBehaviour>().HealthSystem;
            Debug.Log(targetHealthSys);
        }

        public override void Tick()
        {
             // Smack this on a timer?
             // Add some particle emission on the timer too?
            
             targetHealthSys.TakeDamage(tickDamage, caster);
        }

        public override void OnRemove()
        {
            
        }
    }
}