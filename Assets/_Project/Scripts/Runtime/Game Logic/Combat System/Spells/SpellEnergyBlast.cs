using System;
using UnityEngine;
using WizardGame.CollisionHandling;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBlast : SpellBase
    {
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

        private OnCollisionEnterGenerateExplosion explosionGenerator;
        private OnCollisionEnterApplyStatusEffectAndDealDamage damager;
        private OnCollisionEnterRemoveObjectIfNotOwner removeIfNotOwner;
        
        private Collision collisionHit = default;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void InitSpell(float explosionRadius
            , float impactRadius, float explosionDmgMult, float impactDmgMult, float travelSpdMult, GameObject caster)
        {
            actualImpactDmg = (int) Math.Round(avgImpactDmg * impactDmgMult);
            actualExplosionDmg = (int) Math.Round(avgExplosionDmg * explosionDmgMult);

            colliderHits = new Collider[maxExplosionTargets];

            this.explosionRadius = avgExplosionRadius * explosionRadius;
            this.caster = caster;

            transform.localScale = Vector3.one * impactRadius;

            var travelSpd = baseTravelSpd * travelSpdMult;
            rb.velocity = caster.transform.forward * (travelSpd * Time.fixedDeltaTime);

            damager = GetComponent<OnCollisionEnterApplyStatusEffectAndDealDamage>();
            explosionGenerator = GetComponent<OnCollisionEnterGenerateExplosion>();
            removeIfNotOwner = GetComponent<OnCollisionEnterRemoveObjectIfNotOwner>();

            removeIfNotOwner.Init(caster);
            damager.Init(spellElement, caster, actualImpactDmg);
            explosionGenerator.Init(caster, spellElement, this.explosionRadius, actualExplosionDmg, colliderHits);
        }

        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude >= 900f) return;

            rb.velocity *= 1.05f;
        }
    }
}