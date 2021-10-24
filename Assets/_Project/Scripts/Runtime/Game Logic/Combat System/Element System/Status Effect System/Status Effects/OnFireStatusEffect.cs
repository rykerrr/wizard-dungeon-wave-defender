using System;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class OnFireStatusEffect : StatusEffectBase
    {
        private int tickDamage;
        
        private IDamageable targetHealthSys;
        private Element element;

        public int TickDamage => tickDamage;

        public override void Init(GameObject caster, GameObject target, Element element, StatusEffectData data)
        {
            base.Init(caster, target, element, data);

            tickDamage = data.DamagePerFrame;
            this.element = element;
            
            Debug.Log(caster + " | " + target);
            
            targetHealthSys = target.GetComponent<IDamageable>();
            
            Debug.Log(targetHealthSys);
        }

        public override void Tick()
        {
             // Smack this on a timer?
             // Add some particle emission on the timer too?
            
             targetHealthSys.TakeDamage(tickDamage, element, caster);
        }

        public override void OnRemove()
        {
            
        }
    }
}