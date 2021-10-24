using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    [Serializable]
    public class GetEntitiesInRadius<T> : GetEntities<T>
    {
        private LayerMask layersToHit;
        
        private ExtractTFromColliders<T> extractor = new ExtractTFromColliders<T>();

        private Vector3 center;
        private float radius;
        
        public GetEntitiesInRadius( LayerMask layersToHit, Vector3 center, float radius)
        {
            this.layersToHit = layersToHit;
            this.center = center;
            this.radius = radius;
        }
        
        public override List<T> GetTs(ref Collider[] colliderHits)
        {
            Debug.Log(center + " | " + radius);
            
            Array.Clear(colliderHits, 0, colliderHits.Length);

            var explosionHits = Physics.OverlapSphereNonAlloc(center, radius,
                colliderHits, layersToHit, QueryTriggerInteraction.Ignore);
            // hit every layer but ignore triggers

            var ts = extractor.GetTFromColliders(explosionHits, colliderHits);

            return ts;
        }
    }
}