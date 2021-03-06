using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Health_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBolt : SpellCastBase
    {
        private EnergyBoltData spellData = default;

        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;

        private HealthSystemBehaviour hitHealthSys = default;
        private Vector3 mouseHitPos = default;
        private Camera mainCam = default;
        private StatBase intStat = default;

        public override BaseSpellCastData Data
        {
            get => spellData;
            protected set
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
            , Guid id, Transform castCirclePlacement, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
            , params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, statsSys, cooldownSys, id, castCirclePlacement, castCircle, data
                , spellPrefab, movementScripts);

            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
            intStat = statsSys.GetStat(StatTypeFactory.GetType("Intelligence"));
        }

        protected override void Awake()
        {
            mainCam = Camera.main;
        }

        protected override IEnumerator StartSpellCast()
        {
            isCasting = true;
         
            Debug.Log($"I CAST UNTO THEE! {isCasting}");
            
            DeactivateMovementScripts();
            castCircle.gameObject.SetActive(true);

            var castCirclePlacementPos = castCirclePlacement.position;
            var ownerForw = ownerTransf.forward;
            
            castCircleTransf.position = castCirclePlacementPos + ownerForw * 2f;
            castCircleTransf.forward = ownerForw;

            mouseHitPos = GetMouseHitPos();
            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;

            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var spawnPos = ownerTransf.position + ownerTransf.up * 2f + ownerTransf.forward;

            CreateSpellObject(spawnPos);

            castCircleAnimator.SetBool(EndCastHash, false);

            EnableCastCooldown();
            ReactivateMovementScripts();

            Debug.Log("Finished casting");
            isCasting = false;
        }

        private void CreateSpellObject(Vector3 spawnPos)
        {
            var spellClone = (SpellEnergyBolt) Instantiate(spellPrefab, spawnPos, Quaternion.identity);
            var spellTransf = spellClone.transform;
            var spellLocalScale = spellTransf.localScale;

            spellTransf.LookAt(mouseHitPos);

            var newScale = new Vector3(spellLocalScale.x, spellLocalScale.y,
                (spawnPos - mouseHitPos).magnitude);

            spellTransf.localScale = newScale;

            var elData = element.ElementSpellData;
            var explSize = spellData.ExplosionSize * elData.ExplosionRadiusMult;
            var explDmg = spellData.BaseExplosionDamage * elData.ExplosionStrengthMult;
            var impactDmg = spellData.BaseImpactDamage * elData.ImpactStrengthMult;

            spellClone.InitSpell(explSize, spellData.ImpactSize
                , explDmg, impactDmg,
                mouseHitPos, hitHealthSys, Owner);
        }

        private Vector3 GetMouseHitPos()
        {
            var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            var didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);

            var rb = hitInfo.collider.attachedRigidbody;
            var targExist = rb != null && (hitHealthSys = rb.gameObject.GetComponent<HealthSystemBehaviour>()) != null;
            if (!targExist && hitInfo.collider != null)
            {
                hitHealthSys = hitInfo.collider.GetComponent<HealthSystemBehaviour>();
            }
            
            if (didHit) return hitInfo.point;

            return mouseRay.direction * 50f;
        }
    }
}