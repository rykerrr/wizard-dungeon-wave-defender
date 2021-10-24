using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Health_System;
using Object = UnityEngine.Object;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBolt : SpellBase, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private Explosion onHitEffect = default;

        [Header("Properties, do not change in prefab variants")] 
        [SerializeField] private LayerMask entitiesLayerMask;
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private HealthSystemBehaviour objHit = default;
        private Collider[] colliderHits;
        
        private Vector3 hitPos = default;

        private int actualExplosionDmg = default; 
        private int actualImpactDmg = default;
        private float actualRadius = default;

        public void InitSpell(float explosionRadius, float impactRadius, float explosionDmgMult
            , float impactDmgMult, Vector3 hitPos, HealthSystemBehaviour objHit, GameObject caster)
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
            GenerateAndProcessExplosion(hitPos);
            
            DealDamageToImpactTarget();

            Destroy(gameObject, 0.3f);
        }

        private void DealDamageToImpactTarget()
        {
            if (ReferenceEquals(objHit, null)) return;
            
            IDamageable hitImpactTarget = default;
            
            var targExists = (hitImpactTarget = objHit.GetComponent<IDamageable>()) != null;
            if (targExists)
            {
                objHit.TakeDamage(actualImpactDmg, SpellElement, caster);
            }

            if (targExists)
            {
                TryApplyStatusEffect(hitImpactTarget);

                hitImpactTarget.TakeDamage(actualImpactDmg, SpellElement, caster);
            }
        }

        private void TryApplyStatusEffect(IDamageable hitImpactTarget)
        {
            var healthBehav = hitImpactTarget as HealthSystemBehaviour;
            if (healthBehav == null) return;
            
            var statEffData = SpellElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                caster, SpellElement, healthBehav.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = healthBehav.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                actualImpactDmg = (int)Math.Round(actualImpactDmg * buff.Effectiveness);
            }
        }
        
        private Explosion GenerateAndProcessExplosion(Vector3 pos)
        {
            var onHitClone = Instantiate(onHitEffect, pos, Quaternion.identity);
            onHitClone.transform.localScale = Vector3.one * actualRadius;

            onHitClone.Init(actualExplosionDmg, actualRadius, SpellElement, 
                Caster, entitiesLayerMask, ref colliderHits);
            
            Debug.Log("on hit clone exists bruv", onHitClone);
            
            return onHitClone;
        }
    }
}
