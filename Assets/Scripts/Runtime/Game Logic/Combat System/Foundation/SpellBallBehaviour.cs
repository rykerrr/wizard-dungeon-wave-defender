using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Mock_Up
{
    public class SpellBallBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffect = default;

        [SerializeField] private float avgSpd = default;
        [SerializeField] private float avgExplosionRadius = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private Rigidbody rb = default;
        private GameObject caster = default;
        private int actualImpactDmg = default;
        private int actualExplosionDmg = default;
        private float actualRadius = default;
        private float actualSpd = default;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void InitSpell(float spdMult, float explosionRadMult
            , float onHitDmgMult, float explosionDmgMult, GameObject caster)
        {
            actualImpactDmg = (int)Math.Round(avgImpactDmg * onHitDmgMult);
            actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            actualRadius = avgExplosionRadius * explosionRadMult;
            actualSpd = avgSpd * spdMult;
            this.caster = caster;

            rb.velocity = caster.transform.forward * actualSpd * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            if (rb.velocity.sqrMagnitude >= 900f) return;

            rb.velocity *= 1.05f;
        }

        private void OnTriggerEnter(Collider other)
        {
            // this is what happens when the spell hits something
            // we now need to get a circlecast to realize whats in the explosion radius
            // deal damage to everything in explosion radius
            // deal onhitdamage to thing we hit

            var explClone = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explClone.transform.localScale = Vector3.one * avgExplosionRadius;

            Collider[] explosionHits = Physics.OverlapSphere(transform.position, actualRadius);
            List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();

            foreach (var target in explosionHits)
            {
                HealthSystemBehaviour behav = default;

                if (!ReferenceEquals(behav = target.GetComponent<HealthSystemBehaviour>(), null))
                {
                    healthSystemBehaviours.Add(behav);
                }
            }

            healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(actualExplosionDmg, caster));

            HealthSystemBehaviour hitTarget;

            if (!ReferenceEquals(hitTarget = other.GetComponent<HealthSystemBehaviour>(), null))
            {
                hitTarget.HealthSystem.TakeDamage(actualImpactDmg, caster);
            }

            Destroy(explClone, 1.5f);
            Destroy(gameObject);
        }
    }
}
