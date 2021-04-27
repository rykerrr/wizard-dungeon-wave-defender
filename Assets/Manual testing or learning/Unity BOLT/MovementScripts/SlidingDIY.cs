using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

#pragma warning disable 0649
[Serializable]
public class SlidingDIY : IMovementModifier
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private float slideSpeed = 2f;

    public Vector3 Value { get; private set; }

    private GravityMovementModifier gravity;
    private RaycastHit lastHit;
    private float maxDistance;

    public void Init()
    {
        maxDistance = charController.height + charController.radius;
        gravity = charController.GetComponent<GravityBehavior>().Gravity;
    }

    public void Tick(float deltaTime)
    {
        Vector3 rayOrigin = charController.transform.position + charController.center;
        
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, maxDistance))
        {
            lastHit = hit;
        }
        
        Vector3 hitNormal = lastHit.normal;
        Vector3 mvVector = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
        
        Vector3.OrthoNormalize(ref mvVector, ref hitNormal);

        Value = mvVector * (slideSpeed * deltaTime);
    }

    public bool ShouldSlide()
    {
        Vector3 rayOrigin = charController.transform.position + charController.center;
        bool raycastHitThisFrame = false;
        
        if ((raycastHitThisFrame = Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, maxDistance)) is true)
        {
            lastHit = hit;
            
            Vector3 hitNormal = lastHit.normal;
            float slopeAngle = Vector3.Angle(Vector3.up, hitNormal);
            bool isAboveSlopeLimit = slopeAngle > charController.slopeLimit;
        
            // Debug.Log(slopeAngle + " | " + charController.isGrounded + " | " + raycastHitThisFrame + " | " + isAboveSlopeLimit);
        
            if (isAboveSlopeLimit)
            {
                return true;
            }
        }
        
        return false;
    }
}