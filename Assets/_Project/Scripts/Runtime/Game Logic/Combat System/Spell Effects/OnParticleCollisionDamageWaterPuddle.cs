using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OnParticleCollisionDamageWaterPuddle : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsPuddle;
        [SerializeField] private int damage = 2;

        private Element element;
        private ParticleSystem pSys;
        private List<ParticleCollisionEvent> collEvents = new List<ParticleCollisionEvent>();

        public void Init(Element element)
        {
            this.element = element;
        }

        private void Awake()
        {
            pSys = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            var collEventsLength = pSys.GetCollisionEvents(other, collEvents);
            var mask = whatIsPuddle.value;
            var layerInBitForm = 1 << other.layer;
            var maskAndLayer = mask & layerInBitForm;

            if (maskAndLayer == layerInBitForm)
            {
                for (int i = 0; i < collEventsLength; i++)
                {
                    // Collision callback happens regardless of whether an object is "sitting" on another object
                    // This attempts to make it work once-per-particle by checking the magnitude of that collision
                    // If it's lower than a specific value it's most likely stationary
                    var collisionTooStationary = collEvents[i].velocity.sqrMagnitude <= 1.2f;
                    if (collisionTooStationary) continue;

                    DamagePuddle(other);
                }
            }
        }

        private void DamagePuddle(GameObject other)
        {
            var hasDamageable = other.TryGetComponent<IDamageable>(out var damageable);
            if (!hasDamageable)
            {
                Debug.Log("has no damageable component");
                return;
            }

            Debug.Log("Damaging water puddle!!!!");
            damageable.TakeDamage(damage, element, gameObject);
        }
    }
}