using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastDirectedEnergyExplosion : SpellCastBase
    {
        [SerializeField] private SpellDirectedEnergyExplosion spellPrefab;

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
        
        public override void Init(GameObject owner, CastPlaceholder castCircle
            , BaseSpellCastData data, params MonoBehaviour[] movementScripts)
        {
            base.Init(owner, castCircle, data, movementScripts);

            ownerTransf = Owner.transform;
            castCircleTransf = castCircle.transform;
            intStat = statsSys.GetStat(StatTypeDB.GetType("Intelligence"));
        }

        private void Awake()
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
            Vector3 spawnPos = transform.position;
            if(data.Location == DirectedEnergyExplosionData.ExplosionLocationType.Mouse) spawnPos = GetMouseHitPos();

            var spellClone = Instantiate(spellPrefab, spawnPos, Quaternion.identity);
            
            spellClone.InitSpell(data.ExplosionSize, data.ExplosionDamage, data.ExplosionAmount
            , spawnPos, Owner);
            
            castCircleAnimator.SetBool(BeginCastHash, false);
            castCircleAnimator.SetBool(EndCastHash, false);
            
            EnableCastCooldown();
            ReactivateMovementScripts();

            isCasting = false;
        }
        
        private Vector3 GetMouseHitPos()
        {
            var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
            var didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);
            
            return didHit ? hitInfo.point : Vector3.zero;
        }
    }
}
