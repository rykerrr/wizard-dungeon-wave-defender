﻿using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    public class GetEntitiesWithoutCaster<T>
    {
        public List<T> GetTsWithoutCaster(GetEntities<T> getEntities, GameObject caster, ref Collider[] colliderHits)
        {
            var ts = getEntities.GetTs(ref colliderHits);

            Debug.Log(ts.Count);
            
            foreach (var obj in ts)
            {
                var mb = obj as MonoBehaviour;

                if (mb?.gameObject == caster)
                {
                    ts.Remove(obj);
                }
            }

            Debug.Log(ts.Count);

            return ts;
        }
    }
}