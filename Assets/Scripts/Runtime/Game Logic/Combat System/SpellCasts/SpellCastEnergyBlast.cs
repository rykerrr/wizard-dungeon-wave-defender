using System.Collections;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBlast : SpellCastBase
    {
        [SerializeField] protected SpellEnergyBlast spellPrefab = default;

        private EnergyBlastData data = default;
        
        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;
        private StatBase intStat = default;

        public override BaseSpellCastData Data
        {
            get => data;
            set
            {
                value ??= new EnergyBlastData();

                if (value is EnergyBlastData newData)
                {
                    data = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBlastData");
            }
        }
        
        public override void Init(GameObject owner, CastPlaceholder castCircle 
            , BaseSpellCastData data, params MonoBehaviour[] movementScripts)
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

            
            for (int i = 0; i < data.BlastAmount; i++)
            {
                var spellClone = Instantiate(spellPrefab, spawnPos, ownerTransf.rotation);
                spellClone.InitSpell(data.ExplosionSize, data.ImpactSize, data.ExplosionDamage, data.ImpactDamage,
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
