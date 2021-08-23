using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastDirectedEnergyExplosion : SpellCastBase
    {
        private DirectedEnergyExplosionData data;
        
        private Transform ownerTransf = default;
        private Transform castCircleTransf = default;
        private StatBase intStat = default;

        private Camera mainCam = default;

        public override BaseSpellCastData Data
        {
            get => data;
            set
            {
                value ??= new DirectedEnergyExplosionData();

                if (value is DirectedEnergyExplosionData newData)
                {
                    data = newData;
                }
                else Debug.LogWarning("Passed data isn't null and can't be cast to DirectedEnergyExplosionData");
            }
        }
        
        public override void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
            , Guid id, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
            ,params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, statsSys, cooldownSys, id, castCircle, data
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
            Vector3 spawnPos = GetSpawnPosition();

            var spellClone = (SpellDirectedEnergyExplosion) Instantiate(spellPrefab, spawnPos, Quaternion.identity);

            var elData = element.ElementSpellData;
            var explSize = data.ExplosionSize * elData.ExplosionRadiusMult;
            var explDmg = data.BaseExplosionDamage * elData.ExplosionStrengthMult;
            
            spellClone.InitSpell(explSize, explDmg
                , data.ExplosionAmount, spawnPos, Owner);
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);
            
            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }

        private Vector3 GetSpawnPosition()
        {
            Vector3 spawnPos = Vector3.zero;
            
            switch (data.Location)
            {
                case DirectedEnergyExplosionData.ExplosionLocationType.Self:
                {
                    spawnPos = transform.position;

                    break;
                }
                case DirectedEnergyExplosionData.ExplosionLocationType.Mouse:
                {
                    spawnPos = GetMouseHitPos();

                    break;
                }
                case DirectedEnergyExplosionData.ExplosionLocationType.NearestEnemy:
                {
                    throw new NotImplementedException();
                }
            }

            return spawnPos;
        }

        private Vector3 GetMouseHitPos()
        {
            var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            var didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);
            
            return didHit ? hitInfo.point : Vector3.zero;
        }
    }
}
