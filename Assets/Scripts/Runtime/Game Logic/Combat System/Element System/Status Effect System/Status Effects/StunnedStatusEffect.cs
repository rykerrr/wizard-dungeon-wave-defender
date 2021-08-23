using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StunnedStatusEffect : StatusEffect
    {
        private readonly List<MovementModifierBehaviour> movements = new List<MovementModifierBehaviour>();
        private readonly List<MonoBehaviour> monoBehavs = new List<MonoBehaviour>();

        public override void Init(GameObject caster, GameObject target, StatusEffectData data)
        {
            Debug.Log("Stunned newb");
            
            base.Init(caster, target, data);
            
            movements.Add(target.GetComponent<LocomotionMovementBehaviour>());
            movements.Add(target.GetComponent<JumpingMovementBehaviour>());
            monoBehavs.Add(target.GetComponent<SpellCastHandler>());

            SetMonoBehavsEnabled(false);
        }

        public override void Tick()
        {
            SetExternalMultToMovements(0);
        }

        public override void OnRemove()
        {
            // reset external multiplier to 1
            
            SetExternalMultToMovements(1f);
            SetMonoBehavsEnabled(true);
        }
        
        private void SetExternalMultToMovements(float extMult)
        {
            foreach (var mv in movements)
            {
                mv.ExternalMult = extMult;
            }
        }

        private void SetMonoBehavsEnabled(bool enabled)
        {
            foreach (var mono in monoBehavs)
            {
                mono.enabled = enabled;
            }
        }
    }
}