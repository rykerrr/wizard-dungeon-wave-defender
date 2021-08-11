using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBolt : SpellCastBase
    {
        private EnergyBoltData spellData = default;

        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;

        private GameObject objHit = default;
        private Vector3 mouseHitPos = default;
        private Camera mainCam = default;
        private StatBase intStat = default;

        public override BaseSpellCastData Data
        {
            get => spellData;
            set
            {
                value ??= new EnergyBoltData();

                if (value is EnergyBoltData newData)
                {
                    spellData = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBoltData");
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
            intStat = statsSys.GetStat(StatTypeDB.GetType("Intelligence"));
        }

        protected override void Awake()
        {
            mainCam = Camera.main;
        }

        protected override IEnumerator StartSpellCast()
        {
            isCasting = true;
            DeactivateMovementScripts();
            castCircle.gameObject.SetActive(true);

            castCircleTransf.position = ownerTransf.position + ownerTransf.forward * 2f;
            castCircleTransf.forward = ownerTransf.forward;

            mouseHitPos = GetMouseHitPos();
            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;

            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var spawnPos = ownerTransf.position + ownerTransf.up * 2f + ownerTransf.forward;

            var spellClone = (SpellEnergyBolt) Instantiate(spellPrefab, spawnPos, Quaternion.identity);
            var spellTransf = spellClone.transform;
            var spellLocalScale = spellTransf.localScale;

            spellTransf.LookAt(mouseHitPos);

            var newScale = new Vector3(spellLocalScale.x, spellLocalScale.y,
                (mouseHitPos - spellTransf.position).magnitude);
            spellTransf.localScale = newScale;

            var elData = element.ElementSpellData;
            var explSize = spellData.ExplosionSize * elData.ExplosionRadiusMult;
            var explDmg = spellData.BaseExplosionDamage * elData.ExplosionStrengthMult;
            var impactDmg = spellData.BaseImpactDamage * elData.ImpactStrengthMult;

            spellClone.InitSpell(explSize, spellData.ImpactSize
                , explDmg, impactDmg,
                mouseHitPos, objHit, Owner);

            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }

        private Vector3 GetMouseHitPos()
        {
            Ray mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            bool didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);

            objHit = null;
            if (hitInfo.collider)
                objHit = hitInfo.collider.gameObject;

            if (didHit) return hitInfo.point;

            return mouseRay.direction * 50f;
        }
    }
}