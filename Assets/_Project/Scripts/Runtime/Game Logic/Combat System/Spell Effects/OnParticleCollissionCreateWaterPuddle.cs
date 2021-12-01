using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace WizardGame.Combat_System.Spell_Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class OnParticleCollissionCreateWaterPuddle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem pSys = default;
        [SerializeField] private GameObject waterPuddlePrefab = default;
        [SerializeField] private int puddleSpawnChance = 25;
        [SerializeField] private LayerMask puddlesCanSpawnOn = default;

        private List<ParticleCollisionEvent> collEvents = new List<ParticleCollisionEvent>();

        private Random rand = default;

        // TODO: Rewrite entire chance thingy to actually work better for chances
        // E.g: 10% chance to spawn a puddle

        private void Awake()
        {
            rand = new Random((int) Time.realtimeSinceStartup);
        }

        private void CreatePuddle(Vector3 pos, Vector3 upDir)
        {
            var puddleClone = Instantiate(waterPuddlePrefab, pos, Quaternion.identity);
            puddleClone.transform.up = upDir;
        }

        private void OnParticleCollision(GameObject other)
        {
            TryCreateWaterPuddle(other);
        }

        private void TryCreateWaterPuddle(GameObject other)
        {
            var collEventsLength = pSys.GetCollisionEvents(other, collEvents);
            var mask = puddlesCanSpawnOn.value;
            var layerInBitForm = 1 << other.layer;
            var maskAndLayer = mask & layerInBitForm;

            Debug.Log($"{other} | {other.layer} | {collEventsLength} {maskAndLayer == layerInBitForm}");

            if (maskAndLayer == layerInBitForm)
            {
                for (int i = 0; i < collEventsLength; i++)
                {
                    // Collision callback happens regardless of whether an object is "sitting" on another object
                    // This attempts to make it work once-per-particle by checking the magnitude of that collision
                    // If it's lower than a specific value it's most likely stationary
                    var collisionTooStationary = collEvents[i].velocity.sqrMagnitude <= 1.2f;
                    if (collisionTooStationary) continue;

                    var num = rand.Next(0, 100);

                    if (num <= puddleSpawnChance)
                    {
                        CreatePuddle(collEvents[i].intersection, collEvents[i].normal);
                    }
                }
            }
        }
    }
}