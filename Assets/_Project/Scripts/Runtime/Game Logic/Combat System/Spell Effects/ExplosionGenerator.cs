using System;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using Object = UnityEngine.Object;

namespace WizardGame.Combat_System.Spell_Effects
{
    [Serializable]
    public class ExplosionGenerator
    {
        [SerializeField] private Explosion onHitEffect = default;
        [SerializeField] private LayerMask entitiesLayerMask = default;

        private GameObject owner;
        private Element element;

        private Collider[] colliderHits;

        private int explosionDmg = default;
        private float explosionRadius = default;
        
        public void Init(GameObject owner, Element element, float explosionRadius, int explosionDmg,
            params Collider[] colliderHits)
        {
            this.owner = owner;
            this.element = element;
            this.explosionRadius = explosionRadius;
            this.explosionDmg = explosionDmg;
            this.colliderHits = colliderHits;
        }
        
        public void GenerateAndProcessExplosion(Vector3 pos)
        {
            if (ReferenceEquals(onHitEffect, null))
            {
                Debug.LogError("On hit effect not set.");
                Debug.Break();

                return;
            }
         
            var explosionClone = Object.Instantiate(onHitEffect, pos, Quaternion.identity);
            explosionClone.transform.localScale = Vector3.one * explosionRadius;

            // increase damage if specific element (status effect interaction)
            explosionClone.Init(explosionDmg, explosionRadius, element
                , owner, entitiesLayerMask, ref colliderHits);
        }
    }
}