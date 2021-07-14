using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public class SpellCastEnergyBolt : SpellCastBase
    {
        [SerializeField] protected SpellEnergyBolt spellPrefab = default;

        private Transform castCircleTransf = default;
        private Transform ownerTransf = default;

        private GameObject objHit = default;
        private Vector3 mouseHitPos = default;
        private Camera mainCam = default;
        private StatBase intStat = default;

        protected void Awake()
        {
            mainCam = Camera.main;
        }

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

            mouseHitPos = GetMouseHitPos();
            castCircleAnimator.SetBool(BeginCastHash, true);

            yield return castingTimeWait;
            
            castCircleAnimator.SetBool(EndCastHash, true);
        }

        public override void FinishSpellCast()
        {
            var spawnPos = ownerTransf.position + ownerTransf.up * 2f + ownerTransf.forward;

            var spellClone = Instantiate(spellPrefab, spawnPos, Quaternion.identity);
            var spellTransf = spellClone.transform;
            var spellLocalScale = spellTransf.localScale;
            
            spellTransf.LookAt(mouseHitPos);
            
            var newScale = new Vector3(spellLocalScale.x, spellLocalScale.y,
                (mouseHitPos - spellTransf.position).magnitude);
            spellTransf.localScale = newScale;
            
            spellClone.InitSpell(1f * SpellCastData.SpellStrength, 1f * SpellCastData.SpellStrength / 2f
                , intStat.ActualValue, mouseHitPos, objHit, Owner);
            
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
            if(hitInfo.collider)
                objHit = hitInfo.collider.gameObject;
            
            if (didHit) return hitInfo.point;

            return mouseRay.direction * 50f;
        }
    }
}
