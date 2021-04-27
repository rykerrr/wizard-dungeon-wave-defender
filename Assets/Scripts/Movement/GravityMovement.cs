using System;
using UnityEngine;

#pragma warning disable 0649
namespace  WizardGame.Movement
{
    [Serializable]
    public class GravityMovement : IMovementModifier
    {
        public Vector3 Value { get; private set; }
        
        private float yVelocity = 0f;
        
        public void Tick(float deltaTime, float gravMagnitude, float groundedPullMagnitude, bool isGrounded)
        {
            yVelocity -= gravMagnitude;
            bool isFalling = yVelocity < 0f;
        
            if (isGrounded && isFalling)
            {
                yVelocity = -groundedPullMagnitude;
            }

            Value = new Vector3(0f, yVelocity, 0f) * deltaTime;
        }
    }
}
