using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    public abstract class GetEntities<T>
    {
        public abstract List<T> GetTs(ref Collider[] colliderHits);
    }
}