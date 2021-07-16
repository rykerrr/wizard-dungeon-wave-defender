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

        private TimedStatBuffData data;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        
        public override BaseSpellCastData Data
        {
            get => data;
            set
            {
                value ??= new TimedStatBuffData();

                if (value is TimedStatBuffData newData)
                {
                    data = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBoltData");
            }
        }
        
        public override void Init(GameObject owner, CastPlaceholder castCircle 
            , BaseSpellCastData data, params MonoBehaviour[] movementScripts)
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

            var modifierToAdd = new StatModifier(statModifier.Type, statModifier.Value * data.BuffStrength
                , Owner);
            
            statsSys.AddTimedModifier(key, modifierToAdd, data.Duration);
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}
