using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    [Serializable]
    public class GetEntitiesInBox<T> : GetEntities<T>
    {
        private LayerMask layersToHit;
        
        private ExtractTFromColliders<T> extractor = new ExtractTFromColliders<T>();

        private Vector3 center;
        private Vector3 halfExtents;
        
        public GetEntitiesInBox(LayerMask layersToHit, Vector3 center, Vector3 halfExtents)
        {
            extractor = new ExtractTFromColliders<T>();

            this.layersToHit = layersToHit;
            this.halfExtents = halfExtents;
            this.center = center;
        }
        
        public override List<T> GetTs(ref Collider[] colliderHits)
        {
            Debug.Log(center + " | " + halfExtents);

            Array.Clear(colliderHits, 0, colliderHits.Length);

            var explosionHits = Physics.OverlapBoxNonAlloc(center, halfExtents,
                colliderHits, Quaternion.identity, layersToHit, QueryTriggerInteraction.Ignore);

            var ts = extractor.GetTFromColliders(explosionHits, colliderHits);

            return ts;
        }
    }
}