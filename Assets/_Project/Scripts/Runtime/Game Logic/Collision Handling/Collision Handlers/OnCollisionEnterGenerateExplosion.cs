using UnityEngine;
using WizardGame.CollisionHandling;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Spell_Effects;

namespace WizardGame.CollisionHandling
{
    public class OnCollisionEnterGenerateExplosion : MonoBehaviour, ICollisionHandler
    {
        [SerializeField] private ExplosionGenerator explGenerator;
        
        public void Init(GameObject owner, Element element, float explosionRadius, int explosionDmg,
            params Collider[] colliderHits)
        {
            explGenerator.Init(owner, element, explosionRadius, explosionDmg, colliderHits);
        }

        public void ProcessCollision(GameObject other, CollisionType type)
        {
            if (type != CollisionType.CollisionEnter && type != CollisionType.TriggerEnter) return;
            
            explGenerator.GenerateAndProcessExplosion(transform.position);
        }
    }
}