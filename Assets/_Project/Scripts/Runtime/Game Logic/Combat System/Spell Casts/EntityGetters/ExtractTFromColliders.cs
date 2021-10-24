using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.EntityGetters
{
    public class ExtractTFromColliders<T>
    {
        public List<T> GetTFromColliders(int count, params Collider[] colliderHits)
        {
            List<T> damageables = new List<T>();

            for (var i = count - 1; i >= 0; i--)
            {
                T damageable = default;

                if (colliderHits[i].attachedRigidbody)
                {
                    if ((damageable = colliderHits[i].attachedRigidbody.GetComponent<T>()) != null)
                    {
                        if (damageables.Contains(damageable)) continue;
                        
                        damageables.Add(damageable);
                        continue;
                    }
                }
                
                if ((damageable = colliderHits[i].GetComponent<T>()) != null)
                {
                    if (damageables.Contains(damageable)) continue;
                    damageables.Add(damageable);
                }
            }

            return damageables;
        }
    }
}