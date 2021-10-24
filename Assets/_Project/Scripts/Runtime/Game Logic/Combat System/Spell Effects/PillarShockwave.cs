using System;
using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class PillarShockwave : MonoBehaviour
    {
        [SerializeField] private Renderer graphics;

        private GetEntitiesInBox<IDamageable> boxEntitiesGetter = default;
        private GetEntitiesWithoutCaster<IDamageable> noCasterEntitiesExtractor = default;
        
        private GameObject caster = default;
        private Element spellElement;
        
        private Collider[] colliderHits = default;

        private Vector3 swExtents;

        private int actualSwDamage = default;

        public void Init(int actualSwDamage, Vector3 swExtents, Element spellElement, GameObject caster,
            LayerMask entitiesLayerMask, ref Collider[] colliderHits)
        {
            this.colliderHits = colliderHits;

            this.actualSwDamage = actualSwDamage;
            this.caster = caster;
            this.swExtents = swExtents;
            this.spellElement = spellElement;

            Material graphMat = graphics.material;

            Color swColor = spellElement.ElementColor;
            graphMat.color = new Color(swColor.r, swColor.g, swColor.b, graphMat.color.a);

            noCasterEntitiesExtractor = new GetEntitiesWithoutCaster<IDamageable>();
            boxEntitiesGetter = new GetEntitiesInBox<IDamageable>(entitiesLayerMask, transform.position, swExtents);

            ProcessShockwave();
        }

        private void ProcessShockwave()
        {
            var healthSysBehavs = noCasterEntitiesExtractor.GetTsWithoutCaster(boxEntitiesGetter, caster, ref colliderHits);

            foreach (var health in healthSysBehavs)
            {
                health.TakeDamage(actualSwDamage, spellElement, caster);
            }
        }

        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }
    }
}