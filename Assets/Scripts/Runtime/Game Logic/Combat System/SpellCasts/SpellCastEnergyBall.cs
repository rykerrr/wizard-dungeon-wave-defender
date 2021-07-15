using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBall : SpellCastBase
    {
        [SerializeField] protected SpellEnergyBall spellPrefab = default;

        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;
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
            
            castCircleTransf.position = ownerTransf.position + ownerTransf.forward * 2f;
            castCircleTransf.forward = ownerTransf.forward;

            castCircleAnimator.SetBool(BeginCastHash, true);
            
            yield return castingTimeWait;

            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            StartCoroutine(MultiCast());
        }

        private IEnumerator MultiCast()
        {
            var waitForDelay = new WaitForSeconds(0.5f);
            var spawnPos = ownerTransf.position + ownerTransf.forward * 2; 

            
            for (int i = 0; i < SpellCastData.CastAmn; i++)
            {
                var spellClone = Instantiate(spellPrefab, spawnPos, ownerTransf.rotation);
                spellClone.InitSpell(1f * SpellCastData.SpeedMultiplier, 1f * SpellCastData.SpellStrength,
                    intStat.ActualValue * SpellCastData.SpellStrength / 2, 1 + 1/intStat.ActualValue,
                    Owner);
                
                yield return waitForDelay;
            }
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}
