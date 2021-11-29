using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    public class GetEntitiesWithoutCaster<T>
    {
        public List<T> GetTsWithoutCaster(GetEntities<T> getEntities, GameObject caster, ref Collider[] colliderHits)
        {
            var ts = getEntities.GetTs(ref colliderHits);

            var objFound = ts.Find(x => (x as MonoBehaviour)?.gameObject == caster);
            
            ts.Remove(objFound);

            return ts;
        }
    }
}