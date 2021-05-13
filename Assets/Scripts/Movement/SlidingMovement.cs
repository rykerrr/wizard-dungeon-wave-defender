using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    [Serializable]
    public class SlidingMovement : IMovementModifier
    {
        [SerializeField] private float slideSpeed;
        [SerializeField] private float groundRayMaxDistance;

        public float SlideSpeed
        {
            get => slideSpeed;
            set => slideSpeed = value;
        }

        public float GroundRayMaxDistance
        {
            get => groundRayMaxDistance;
            set => groundRayMaxDistance = value;
        }
        // ground ray to character motor

        public Vector3 Value { get; private set; }

        private RaycastHit lastHit;

        public void Tick(float deltaTime, Vector3 rayOrigin)
        {
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundRayMaxDistance))
            {
                lastHit = hit;
            }

            Vector3 hitNormal = lastHit.normal;
            Vector3 mvVector = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);

            Vector3.OrthoNormalize(ref mvVector, ref hitNormal);

            Value = mvVector * (slideSpeed * deltaTime);
        }

        public bool ShouldSlide(Vector3 rayOrigin, float slopeLimit)
        {
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundRayMaxDistance))
            {
                lastHit = hit;

                Vector3 hitNormal = lastHit.normal;
                float slopeAngle = Vector3.Angle(Vector3.up, hitNormal);
                bool isAboveSlopeLimit = slopeAngle > slopeLimit;

                if (isAboveSlopeLimit)
                {
                    return true;
                }
            }

            return false;
        }
    }
}