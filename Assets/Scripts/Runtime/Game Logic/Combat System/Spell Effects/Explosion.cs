using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private FadeMeshRendererOverLifetime fader = default;
    
        private GameObject caster;
        
        private Collider[] colliderHits;
        private float explosionRadius;
        private int explosionDamage;

        private float objLifetime = 0f;

        public void Init(int explosionDamage, float explosionRadius, Color explColor, GameObject caster
            , ref Collider[] colliderHits)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.caster = caster;
            this.colliderHits = colliderHits;

            fader.CustomColor = explColor;
            
            ProcessExplosion();
        }

        protected virtual void ProcessExplosion()
        {
            var healthSystemBehaviours = GetHealthSystemsInRadiusIgnoreCaster();
            healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(explosionDamage, caster));
        }
        
        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }
        
        private List<HealthSystemBehaviour> GetHealthSystemsInRadiusIgnoreCaster()
        {
            Array.Clear(colliderHits, 0, colliderHits.Length);
            
            var explosionHits = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, 
                colliderHits, ~0, QueryTriggerInteraction.Ignore);
            // hit every layer but ignore triggers
            
            var healthSystemBehaviours = ReturnHealthSystemsFromColliders(explosionHits);
            
            return healthSystemBehaviours;
        }

        private List<HealthSystemBehaviour> ReturnHealthSystemsFromColliders(int explosionHits)
        {
            List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();
            
            for (var i = explosionHits - 1; i >= 0; i--)
            {
                if (colliderHits[i].gameObject == caster) continue;

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