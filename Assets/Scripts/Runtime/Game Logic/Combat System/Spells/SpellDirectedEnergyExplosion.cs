using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System
{
    public class SpellDirectedEnergyExplosion : MonoBehaviour, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private GameObject explosionEffect = default;

        [Header("Properties")]
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgExplosionDmg = default;

        private GameObject caster = default;

        private int actualExplosionDmg = default;
        private float actualRadius = default;

        public void InitSpell(float explosionRadMult, float explosionDmgMult
            , Vector3 explPos, GameObject caster)
        {
            actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            actualRadius = avgExplosionRadius * explosionRadMult;

            this.caster = caster;

            ProcessOnHit();
        }

        public void ProcessOnHit()
        {
            var explClone = GenerateAndProcessExplosion(transform.position);
            
            Destroy(explClone, 1.5f);
            Destroy(gameObject, 0.3f);
        }

        private GameObject GenerateAndProcessExplosion(Vector3 pos)
        {
            var explClone = Instantiate(explosionEffect, pos, Quaternion.identity);
            explClone.transform.localScale = Vector3.one * avgExplosionRadius;

            var healthSystemBehaviours = GetHealthSystemsInRadiusIgnoreCaster();
            healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(actualExplosionDmg, caster));

            return explClone;
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInRadiusIgnoreCaster()
        {
            var colliderHits = new Collider[maxExplosionTargets];
            var explosionHits = Physics.OverlapSphereNonAlloc(transform.position, actualRadius, colliderHits);
            var healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = explosionHits - 1; i >= 0; i--)
            {
                if (colliderHits[i].gameObject == caster)
                {
                    continue;
                }
                
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
