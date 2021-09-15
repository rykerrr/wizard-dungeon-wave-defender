using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBolt : SpellBase, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private Explosion onHitEffect = default;

        [Header("Properties, do not change in prefab variants")]
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private GameObject objHit = default;
        private Collider[] colliderHits;
        
        private Vector3 hitPos = default;

        private int actualExplosionDmg = default; 
        private int actualImpactDmg = default;
        private float actualRadius = default;

        public void InitSpell(float explosionRadius, float impactRadius, float explosionDmgMult
            , float impactDmgMult, Vector3 hitPos, GameObject objHit, GameObject caster)
        {
            actualImpactDmg = (int)Math.Round(avgImpactDmg * impactDmgMult);
            actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            actualRadius = avgExplosionRadius * explosionRadius;

            colliderHits = new Collider[maxExplosionTargets];

            this.objHit = objHit;
            this.caster = caster;
            this.hitPos = hitPos;
            
            ProcessOnHitEffect();
        }

        public void ProcessOnHitEffect()
        {
            var explClone = GenerateAndProcessExplosion(hitPos);
            
            HealthSystemBehaviour hitImpactTarget = default;

            var targExists = !ReferenceEquals(objHit, null) &&
                                             !ReferenceEquals(
                                                 hitImpactTarget = objHit.GetComponent<HealthSystemBehaviour>()
                                                 , null);
            if (targExists)
            {
                TryApplyStatusEffect(hitImpactTarget);
                
                hitImpactTarget.HealthSystem.TakeDamage(actualImpactDmg, SpellElement, caster);
            }
            
            Destroy(gameObject, 0.3f);
        }
        
        private Explosion GenerateAndProcessExplosion(Vector3 pos)
        {
            var onHitClone = Instantiate(onHitEffect, pos, Quaternion.identity);
            onHitClone.transform.localScale = Vector3.one * actualRadius;

            onHitClone.Init(actualExplosionDmg, actualRadius, SpellElement, Caster, ref colliderHits);
            
            return onHitClone;
        }
        
        private void TryApplyStatusEffect(HealthSystemBehaviour hitImpactTarget)
        {
            var statEffData = SpellElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                caster, SpellElement, hitImpactTarget.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = hitImpactTarget.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                actualImpactDmg = (int)Math.Round(actualImpactDmg * buff.Effectiveness);
            }
        }
    }
}