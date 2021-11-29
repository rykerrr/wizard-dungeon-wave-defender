using UnityEngine;
using WizardGame.CollisionHandling;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Spell_Effects;

namespace WizardGame.CollisionHandling
{
    public class OnCollisionEnterGenerateExplosion : MonoBehaviour, ICollisionHandler
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

        private void GenerateAndProcessExplosion(Vector3 pos)
        {
            var hitClone = Instantiate(onHitEffect, pos, Quaternion.identity);
            hitClone.transform.localScale = Vector3.one * explosionRadius;

            // increase damage if specific element (status effect interaction)
            hitClone.Init(explosionDmg, explosionRadius, element
                , owner, entitiesLayerMask, ref colliderHits);
        }

        public void ProcessCollision(GameObject other, CollisionType type)
        {
            if (type != CollisionType.CollisionEnter && type != CollisionType.TriggerEnter) return;
            
            GenerateAndProcessExplosion(transform.position);
        }
    }
}