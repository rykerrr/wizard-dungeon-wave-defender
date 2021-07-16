using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBolt : MonoBehaviour, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private GameObject explosionEffect = default;

        [Header("Properties")]
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private GameObject objHit = default;
        private GameObject caster = default;
        
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

            transform.localScale = new Vector3(impactRadius, impactRadius, transform.localPosition.z);
            
            this.objHit = objHit;
            this.caster = caster;
            this.hitPos = hitPos;
            
            ProcessOnHit();
        }

        public void ProcessOnHit()
        {
            var explClone = GenerateAndProcessExplosion(hitPos);
            
            HealthSystemBehaviour hitImpactTarget = default;

            var targetExistsAndCanBeDamaged = !ReferenceEquals(objHit, null) &&
                                             !ReferenceEquals(
                                                 hitImpactTarget = objHit.GetComponent<HealthSystemBehaviour>(), null);
            if (targetExistsAndCanBeDamaged)
            {
                hitImpactTarget.HealthSystem.TakeDamage(actualImpactDmg, caster);
            }
            
            Destroy(explClone, 1.5f);
            Destroy(gameObject, 0.3f);
        }
        
        private GameObject GenerateAndProcessExplosion(Vector3 pos)
        {
            var explClone = Instantiate(explosionEffect, pos, Quaternion.identity);
            explClone.transform.localScale = Vector3.one * actualExplosionDmg;

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

    }
}
