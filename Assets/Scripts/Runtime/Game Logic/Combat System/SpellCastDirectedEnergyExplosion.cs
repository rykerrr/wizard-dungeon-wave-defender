using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastDirectedEnergyExplosion : SpellCastBase
    {
        [SerializeField] private GameObject spellPrefab;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        private StatBase intStat = default;

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
            var spawnPos = ownerTransf.position;

            var spellClone = Instantiate(spellPrefab, spawnPos, Quaternion.identity);
            
            spellClone.GetComponent<SpellDirectedEnergyExplosion>().InitSpell(2f, intStat.ActualValue / 2f,
                spawnPos, gameObject);
            
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
            intStat = statsSys.GetStat(StatTypeDB.GetType("Intelligence"));
        }
    }
}
