using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
[Serializable]
public class LocomotionMovementModifier : IMovementModifier
{
    public float mvSpeed = 50f;
    
    public Vector3 Value { get; private set; }
    private Vector3 previousInput = Vector3.zero;
    
    public void SetPreviousMovementInput(InputAction.CallbackContext ctx)
    {
        Vector2 curInput = ctx.ReadValue<Vector2>();
        previousInput = new Vector3(curInput.x, 0f, curInput.y);
    }

    public void Tick(float deltaTime)
    {
        Value = previousInput * (mvSpeed * deltaTime);
    }
}
