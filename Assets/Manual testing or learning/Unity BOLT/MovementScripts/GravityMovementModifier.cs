using System;
using UnityEngine;

#pragma warning disable 0649
[Serializable]
public class GravityMovementModifier : IMovementModifier
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private float magnitude = -3f;
    [SerializeField] private float groundedPullmagnitude = 30f;

    public Vector3 Value { get; private set; }

    private float yVelocity = 0f;

    public void Tick(float deltaTime)
    {
        yVelocity += magnitude;
        bool isFalling = yVelocity < 0f;
        
        if (charController.isGrounded && isFalling)
        {
            yVelocity = -groundedPullmagnitude;
        }

        Value = new Vector3(0f, yVelocity, 0f) * deltaTime;
    }
}
