using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Debugging
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OnParticleCollisionDrawRayFromIntersectionTowardsVelocity : MonoBehaviour
    {
        [SerializeField] private float rayLifeTime = 0.4f;

        private List<ParticleCollisionEvent> collEvents = new List<ParticleCollisionEvent>();
        private ParticleSystem pSys;

        private void Awake()
        {
            pSys = GetComponent<ParticleSystem>();
        }

        private void OnParticleCollision(GameObject other)
        {
            var collEventsLength = pSys.GetCollisionEvents(other, collEvents);
            
            for (int i = 0; i < collEventsLength; i++)
            {
                Debug.DrawRay(collEvents[i].intersection, collEvents[i].velocity, Color.red, rayLifeTime);
            }
        }
    }
}