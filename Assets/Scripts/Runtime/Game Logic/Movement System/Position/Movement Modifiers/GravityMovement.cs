using System;
using System.Runtime.CompilerServices;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class GravityMovement : IMovementModifier
    {
        [SerializeField] private float gravMagnitude;
        [SerializeField] private float groundedPullMagnitude;

        private float externalMult = 1f;

        public float ExternalMult
        {
            get => externalMult;
            set => externalMult = value;
        }
        
        public float GravMagnitude
        {
            get => gravMagnitude;
            set => gravMagnitude = value;
        }

        public float GroundedPullMagnitude
        {
            get => groundedPullMagnitude;
            set => groundedPullMagnitude = value;
        }

        public Vector3 Value { get; private set; }

        private float yVelocity = 0f;

        public void Tick(float deltaTime, bool isGrounded)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            yVelocity -= gravMagnitude;

            if (isGrounded)
            {
                yVelocity = -groundedPullMagnitude;
            }

            Value = new Vector3(0f, yVelocity, 0f) * deltaTime * ExternalMult;
        }
    }
}