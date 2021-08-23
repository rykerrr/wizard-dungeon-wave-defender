using System;
using System.Collections;
using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyHealingField : SpellCastBase
    {
        private EnergyHealingFieldData data;

        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        private StatBase intStat = default;

        public override BaseSpellCastData Data
        {
            get => data;
            set
            {
                value ??= new EnergyHealingFieldData();

                if (value is EnergyHealingFieldData newData)
                {
                    data = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyHealingFieldData");
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

            castCircleTransf.position = ownerTransf.position;
            castCircleTransf.forward = ownerTransf.up;

            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;

            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var spellClone =
                (SpellEnergyHealingField) Instantiate(spellPrefab, ownerTransf.position, Quaternion.identity);

            var healPerTick = data.TickHeal * element.ElementSpellData.HealStrengthMult;

            spellClone.InitSpell(healPerTick, data.TickAmount, Owner);

            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
    }
}