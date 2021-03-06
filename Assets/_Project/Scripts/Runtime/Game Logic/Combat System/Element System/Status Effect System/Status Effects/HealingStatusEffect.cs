using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class HealingStatusEffect : StatusEffectBase
    {
        private IHealable targetHealthSys;
        private int healPerTick = 0;
        
        public override void Init(GameObject caster, GameObject target, Element element, StatusEffectData data)
        {
            base.Init(caster, target, element, data);

            healPerTick = data.DamagePerFrame;
            
            targetHealthSys = target.GetComponent<IHealable>();
        }
        
        public override void Tick()
        {
            targetHealthSys.Heal(healPerTick, caster);
        }

        public override void OnRemove()
        {
            throw new System.NotImplementedException();
        }
    }
}