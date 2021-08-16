using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StunnedStatusEffect : BaseStatusEffect
    {
        
        
        public override void Init(GameObject caster, GameObject target, ElementStatusEffectData data)
        {
            base.Init(caster, target, data);
            
            // set externalMultiplier to 0 on given movement systems
        }

        public override void Tick()
        {
            return;
        }
        
        public override void OnRemove()
        {
            // reset external multiplier to 1
        }
    }
}