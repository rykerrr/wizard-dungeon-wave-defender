using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class PillarShockwave : MonoBehaviour
    {
        [SerializeField] private Renderer graphics;
        
        private GameObject caster = default;
        
        private Collider[] colliderHits = default;

        private Vector3 swExtents;

        private int actualSwDamage = default;

        public void Init(int actualSwDamage, Vector3 swExtents, Color swColor, GameObject caster
            , ref Collider[] colliderHits)
        {
            this.colliderHits = colliderHits;

            this.actualSwDamage = actualSwDamage;
            this.caster = caster;
            this.swExtents = swExtents;

            Material graphMat = graphics.material;
            graphMat.color = new Color(swColor.r, swColor.g, swColor.b, graphMat.color.a);
            
            ProcessShockwave();
        }

        private void ProcessShockwave()
        {
            var healthSysBehavs = GetHealthSystemsInBoxIgnoreCaster();
            healthSysBehavs.ForEach(x => x.HealthSystem.TakeDamage(actualSwDamage, caster));
        }

        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInBoxIgnoreCaster()
        {
            Array.Clear(colliderHits, 0, colliderHits.Length);

            var shockwaveHits = Physics.OverlapBoxNonAlloc(transform.position, swExtents, colliderHits,
                Quaternion.identity,
                ~0, QueryTriggerInteraction.Ignore);
            // hit every layer but ignore triggers

            var healthSystemBehaviours = GetHealthSystemsFromColliders(shockwaveHits);

            return healthSystemBehaviours;
        }

        private List<HealthSystemBehaviour> GetHealthSystemsFromColliders(int shockwaveHits)
        {
            List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();
            
            for (var i = shockwaveHits - 1; i >= 0; i--)
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