using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBlast : SpellBase, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private Explosion onHitEffect = default;

        [Header("Properties, do not change in prefab variants")]
        [SerializeField] private float baseTravelSpd = default;
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private Rigidbody rb = default;
        private Collider[] colliderHits;
        
        private int actualImpactDmg = default;
        private int actualExplosionDmg = default;
        private float explosionRadius = default;

        private Collision collisionHit = default;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void InitSpell(float explosionRadius
            , float impactRadius, float explosionDmgMult, float impactDmgMult, float travelSpdMult, GameObject caster)
        {
            actualImpactDmg = (int)Math.Round(avgImpactDmg * impactDmgMult);
            actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);

            colliderHits = new Collider[maxExplosionTargets];
            
            this.explosionRadius = avgExplosionRadius * explosionRadius;
            this.caster = caster;

            transform.localScale = Vector3.one * impactRadius;

            var travelSpd = baseTravelSpd * travelSpdMult;
            rb.velocity = caster.transform.forward * (travelSpd * Time.fixedDeltaTime);
        }

        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude >= 900f) return;

            rb.velocity *= 1.05f;
        }

        public void ProcessOnHitEffect()
        {
            #if UNITY_EDITOR
                Debug.Log($"Object {gameObject.name} owned by {caster.name} hit {collisionHit}"); 
            #endif
            
            var explClone = GenerateAndProcessExplosion(transform.position);

            HealthSystemBehaviour hitImpactTarget = default;
            var targExist = (collisionHit.rigidbody) != null && (hitImpactTarget = collisionHit.rigidbody.GetComponent<HealthSystemBehaviour>()) != null;
            if (!targExist)
            {
                targExist = !ReferenceEquals(hitImpactTarget = collisionHit.gameObject.GetComponent<HealthSystemBehaviour>()
                    , null);
            }
            
            if (targExist)
            {
                TryApplyStatusEffect(hitImpactTarget);

                hitImpactTarget.HealthSystem.TakeDamage(actualImpactDmg, SpellElement, caster);
            }
            
            Destroy(gameObject);
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

        private Explosion GenerateAndProcessExplosion(Vector3 pos)
        {
            var onHitClone = Instantiate(onHitEffect, pos, Quaternion.identity);
            onHitClone.transform.localScale = Vector3.one * explosionRadius;

            // increase damage if specific element (status effect interaction)
            onHitClone.Init(actualExplosionDmg, explosionRadius, SpellElement
                , Caster, ref colliderHits);

            return onHitClone;
        }

        public void OnCollisionEnter(Collision other)
        {
            Debug.Log("Tremble in collisions!");
        
            if(!ReferenceEquals(other, null)) collisionHit = other;
            
            ProcessOnHitEffect();
        }
    }
}