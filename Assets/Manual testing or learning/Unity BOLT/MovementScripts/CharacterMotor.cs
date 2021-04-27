using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    
    private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

    public Vector3 GroundNormal { get; private set; } = Vector3.zero;

    private Vector3 lastHitPoint = Vector3.zero;
    private Vector3 lastGroundNormal = Vector3.zero;
    
    public void AddModifier(IMovementModifier modifier) => modifiers.Add(modifier);
    public bool RemoveModifier(IMovementModifier modifier) => modifiers.Remove(modifier);
    private void Update() => Tick(Time.deltaTime);
    private void Tick(float deltaTime)
    {
        Vector3 currentFrameMovement = CalculateCurrentFrameMovement();

        characterController.Move(currentFrameMovement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((hit.normal.y > 0f) && (hit.normal.y > GroundNormal.y) && (hit.moveDirection.y < 0))
        {
            if ((hit.point - lastHitPoint).sqrMagnitude > 0.001f || lastGroundNormal == Vector3.zero)
            {
                GroundNormal = hit.normal;
            }
            else
            {
                GroundNormal = lastGroundNormal;
            }

            lastHitPoint = hit.point;
        }
    }

    private Vector3 CalculateCurrentFrameMovement()
    {
        Vector3 currentFrameMovement = Vector3.zero;
        
        for (int i = modifiers.Count - 1; i >= 0; i--)
        {
            currentFrameMovement += modifiers[i].Value;
        }

        GroundNormal = Vector3.zero;
        
        return currentFrameMovement;

        lastGroundNormal = GroundNormal;
    }
}
