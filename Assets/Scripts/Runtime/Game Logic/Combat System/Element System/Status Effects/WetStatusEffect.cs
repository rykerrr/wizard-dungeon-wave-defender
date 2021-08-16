using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class WetStatusEffect : BaseStatusEffect
    {
        public override void Init(GameObject caster, GameObject target, ElementStatusEffectData data)
        {
            base.Init(caster, target, data);
        }

        public override void Tick()
        {
            throw new System.NotImplementedException();
        }
        
        public override void OnRemove()
        {
            
        }
    }
}