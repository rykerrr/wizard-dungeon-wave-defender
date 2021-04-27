using System;
using UnityEngine;

#pragma warning disable 0649
[Serializable]
public class SlidingMovementModifier : IMovementModifier
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private CharacterController charController;
    [SerializeField] private float slideSpeed;
    
    public Vector3 Value { get; private set; }

    private Vector3 groundNormal = Vector3.zero;
    private float groundRayDistance = 0f;

    public void Init()
    {
        groundRayDistance = charController.height * 0.5f + charController.radius;
        Debug.Log(groundRayDistance);
    }
    
    public void Tick(float deltaTime)
    {
        groundNormal = motor.GroundNormal;

        bool hitSurface = Physics.Raycast(charController.transform.position + charController.center, Vector3.down,
            out RaycastHit hit, groundRayDistance);

        if (hitSurface)
        {
            groundNormal = hit.normal;
        }

        Vector3 movement = new Vector3(groundNormal.x, -groundNormal.y, groundNormal.z);
        Vector3.OrthoNormalize(ref groundNormal, ref movement);

        movement *= slideSpeed * deltaTime;

        Value = movement;
    }

    public bool ShouldSlide()
    {
        if(charController.isGrounded)
        {
            bool hitSurface = Physics.Raycast(charController.transform.position + charController.center, Vector3.down,
                out RaycastHit hit, groundRayDistance);

            Debug.DrawRay(charController.transform.position + charController.center, Vector3.down * 300f, Color.red);

            if (hitSurface)
            {
                float angleWithSlope = Vector3.Angle(hit.normal, Vector3.up);

                bool overSlopeLimit = angleWithSlope > charController.slopeLimit;

                return overSlopeLimit;
            }

            return motor.GroundNormal.y > 0.01f &&
                   motor.GroundNormal.y <= Mathf.Cos(charController.slopeLimit * Mathf.Deg2Rad);
        }

        return false;
    }
}