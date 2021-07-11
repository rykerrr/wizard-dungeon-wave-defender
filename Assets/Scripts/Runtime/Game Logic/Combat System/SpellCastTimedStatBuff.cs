using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastTimedStatBuff : SpellCastBase
    {
        [SerializeField] private StatModifier modifier = default;
        [SerializeField] private string keyName = default;
        [SerializeField] private float statModifierDuration = default;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        
        protected override IEnumerator StartSpellCast()
        {
            if (!CanCast)
            {
#if UNITY_EDITOR
                Debug.Log($"Can't cast due to: Timer Enabled: {castCooldownTimer.IsTimerEnabled}" +
                          $" | Is casting: {isCasting} | Cooldown for cast left: {castCooldownTimer.Time}");
#endif
                
                yield break;
            }
            
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
            statsSys.AddTimedModifier(key, modifier, statModifierDuration);
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
        
        public override void Init(GameObject owner, CastPlaceholder castCircle 
            , SpellCastData data, params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, castCircle, data, movementScripts);
            
            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
        }
    }
}
