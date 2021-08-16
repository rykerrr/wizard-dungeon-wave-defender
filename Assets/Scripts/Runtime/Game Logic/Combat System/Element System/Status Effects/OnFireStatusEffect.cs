using System;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [Serializable]
    public class OnFireStatusEffect : BaseStatusEffect
    {
        [SerializeField] private int tickDamage;
        
        private HealthSystem targetHealthSys;

        public int TickDamage => tickDamage;

        public override void Init(GameObject caster, GameObject target, ElementStatusEffectData data)
        {
            base.Init(caster, target, data);

            tickDamage = data.DamagePerFrame;
            
            targetHealthSys = target.GetComponent<HealthSystemBehaviour>().HealthSystem;
        }

        public override void Tick()
        {
             targetHealthSys.TakeDamage(tickDamage);
        }

        public override void OnRemove()
        {
            
        }
    }
}