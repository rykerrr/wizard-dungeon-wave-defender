using System;
using System.Collections;
using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBlast : SpellCastBase
    {
        private EnergyBlastData data = default;

        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;
        private StatBase intStat = default;

        private Vector3 NextSpawnPos { get; set; }
        private Quaternion NextSpawnRotation { get; set; }
        
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

        public override void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
            , Guid id, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
            , params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, statsSys, cooldownSys, id, castCircle, data
                , spellPrefab, movementScripts);

            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
            intStat = statsSys.GetStat(StatTypeFactory.GetType("Intelligence"));
        }

        protected override IEnumerator StartSpellCast()
        {
            isCasting = true;
            DeactivateMovementScripts();
            castCircle.gameObject.SetActive(true);

            var ownerPos = ownerTransf.position;
            var ownerForw = ownerTransf.forward;
            
            castCircleTransf.position = ownerPos + ownerForw * 2f;
            castCircleTransf.forward = ownerForw;
            
            NextSpawnPos = ownerPos + ownerForw * 2;
            NextSpawnRotation = ownerTransf.rotation;
            
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

            var elData = element.ElementSpellData;
            var explSize = data.ExplosionSize * elData.ExplosionRadiusMult;
            var explDmg = data.BaseExplosionDamage * elData.ExplosionStrengthMult;
            var impactDmg = data.BaseImpactDamage * elData.ImpactStrengthMult;

            for (var i = 0; i < data.BlastAmount; i++)
            {
                var spellClone = (SpellEnergyBlast) Instantiate(spellPrefab, NextSpawnPos, NextSpawnRotation);
                spellClone.InitSpell(explSize, data.ImpactSize
                    , explDmg, impactDmg,
                    elData.TravelSpeedMult, Owner);

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