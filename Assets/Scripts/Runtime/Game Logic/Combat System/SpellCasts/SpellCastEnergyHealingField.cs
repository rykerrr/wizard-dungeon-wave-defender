using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyHealingField : SpellCastBase
    {
        [SerializeField] private SpellEnergyHealingField spellPrefab;

        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        private StatBase intStat = default;

        public override void Init(GameObject owner, CastPlaceholder castCircle
            , SpellCastData data, params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, castCircle, data, movementScripts);

            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
            intStat = statsSys.GetStat(StatTypeDB.GetType("Intelligence"));
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
            var spellClone = Instantiate(spellPrefab, ownerTransf.position, Quaternion.identity);

            spellClone.InitSpell(intStat.ActualValue / 5f, SpellCastData.CastAmn
                , Owner, ownerTransf.position);

            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}
