using System.Collections.Generic;
using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class WetStatusEffect : StatusEffect
    {
        private readonly List<MovementModifierBehaviour> movements = new List<MovementModifierBehaviour>();
        
        public override void Init(GameObject caster, GameObject target, StatusEffectData data)
        {
            base.Init(caster, target, data);
            
            movements.Add(target.GetComponent<LocomotionMovementBehaviour>());
            movements.Add(target.GetComponent<JumpingMovementBehaviour>());

            SetExternalMultToMovements(data.MovementSpeedMultiplier);
        }

        public override void Tick()
        {
            // throw out water particles? though it'd be better if we created a timer that ran it every x seconds instead
            // of each frame
            
            return;
        }

        public override void OnRemove()
        {
            // reset external multiplier to 1
            
            SetExternalMultToMovements(1f);
        }
        
        private void SetExternalMultToMovements(float extMult)
        {
            foreach (var mv in movements)
            {
                mv.ExternalMult = extMult;
            }
        }
    }
}