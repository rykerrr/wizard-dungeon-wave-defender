using System;
using System.Collections;
using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastTimedStatBuff : SpellCastBase
    {
        [SerializeField] private StatModifier statModifier = default;
        [SerializeField] private string keyName = default;
        [SerializeField] private float statModifierDurationMult = 1.2f;

        private TimedStatBuffData data;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        
        public override BaseSpellCastData Data
        {
            get => data;
            protected set
            {
                value ??= new TimedStatBuffData();

                if (value is TimedStatBuffData newData)
                {
                    data = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBoltData");
            }
        }
        
        public override void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
            , Guid id, Transform castCirclePlacement, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
            ,params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, statsSys, cooldownSys, id, castCirclePlacement, castCircle, data
                , spellPrefab, movementScripts);

            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
        }
        
        protected override IEnumerator StartSpellCast()
        {
            isCasting = true;
            DeactivateMovementScripts();
            castCircle.gameObject.SetActive(true);

            castCircleTransf.position = castCirclePlacement.position;
            castCircleTransf.forward = ownerTransf.up;
            
            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;

            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var key = StatTypeFactory.GetType(keyName);

            var buffVal = statModifier.Value * data.BuffStrength * element.ElementSpellData.BuffStrengthMult;
            
            var modifierToAdd = new StatModifier(statModifier.Type, buffVal
                , Owner.name);
            
            statsSys.AddTimedModifier(key, modifierToAdd, data.Duration * statModifierDurationMult);
            
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}
