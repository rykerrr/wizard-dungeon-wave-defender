using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    public class GetEntitiesWithoutCaster<T>
    {
        public List<T> GetTsWithoutCaster(GetEntities<T> getEntities, GameObject caster, ref Collider[] colliderHits)
        {
            var ts = getEntities.GetTs(ref colliderHits);

            foreach (var obj in ts)
            {
                var mb = obj as MonoBehaviour;

                if (mb?.gameObject == caster)
                {
                    ts.Remove(obj);
                }
            }

            return ts;
        }
    }
}