using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastTimedStatBuff : SpellCastBase
    {
        [SerializeField] private StatModifier statModifier = default;
        [SerializeField] private string keyName = default;
        [SerializeField] private float statModifierDuration = default;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        
        public override void Init(GameObject owner, CastPlaceholder castCircle 
            , SpellCastData data, params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, castCircle, data, movementScripts);
            
            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
        }
        
        protected override IEnumerator StartSpellCast()
        {
            isCasting = true;
            DeactivateMovementScripts();
            castCircle.gameObject.SetActive(true);

            castCircleTransf.position = ownerTransf.position;
            castCircleTransf.forward = ownerTransf.up;
            
            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;

            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var key = StatTypeDB.GetType(keyName);

            var modifierToAdd = new StatModifier(statModifier.Type, statModifier.Value * SpellCastData.SpellStrength
                , Owner);
            
            statsSys.AddTimedModifier(key, modifierToAdd, statModifierDuration);
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}
