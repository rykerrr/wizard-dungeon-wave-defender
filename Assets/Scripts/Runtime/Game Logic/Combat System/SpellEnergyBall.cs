using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Health_System;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBall : MonoBehaviour, IDamagingSpell
    {
        [SerializeField] private GameObject explosionEffect = default;

        [SerializeField] private float avgSpd = default;
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private Rigidbody rb = default;
        private GameObject caster = default;
        
        private int actualImpactDmg = default;
        private int actualExplosionDmg = default;
        private float actualRadius = default;
        private float actualSpd = default;

        private GameObject collissionHit = default;
        
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

        public void ProcessOnHit()
        {
            #if UNITY_EDITOR
                Debug.Log($"Object {gameObject.name} owned by {caster.name} hit {collissionHit}"); 
            #endif
            
            var explClone = GenerateAndProcessExplosion(transform.position);

            HealthSystemBehaviour hitImpactTarget;

            if (!ReferenceEquals(hitImpactTarget = collissionHit.GetComponent<HealthSystemBehaviour>(), null))
            {
                hitImpactTarget.HealthSystem.TakeDamage(actualImpactDmg, caster);
            }

            Destroy(explClone, 1.5f);
            Destroy(gameObject);
        }

        private GameObject GenerateAndProcessExplosion(Vector3 pos)
        {
            var explClone = Instantiate(explosionEffect, pos, Quaternion.identity);
            explClone.transform.localScale = Vector3.one * avgExplosionRadius;

            var healthSystemBehaviours = GetHealthSystemsInRadius();
            healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(actualExplosionDmg, caster));
            
            return explClone;
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInRadius()
        {
            var colliderHits = new Collider[maxExplosionTargets];
            var explosionHits = Physics.OverlapSphereNonAlloc(transform.position, actualRadius, colliderHits);
            var healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = explosionHits - 1; i >= 0; i--)
            {
                HealthSystemBehaviour behav = default;

                if (!ReferenceEquals(behav = colliderHits[i].GetComponent<HealthSystemBehaviour>(), null))
                {
                    healthSystemBehaviours.Add(behav);
                }
            }

            return healthSystemBehaviours;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(!ReferenceEquals(other, null)) collissionHit = other.gameObject;

            ProcessOnHit();
        }
        
        public void OnCollisionEnter(Collision other)
        {
            if(!ReferenceEquals(other, null)) collissionHit = other.gameObject;
            
            ProcessOnHit();
        }
    }
}