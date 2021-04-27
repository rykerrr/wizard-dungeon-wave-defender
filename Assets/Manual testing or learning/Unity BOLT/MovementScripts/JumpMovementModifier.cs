using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
[Serializable]
public class JumpMovementModifier : IMovementModifier
{
    [SerializeField] private CharacterController charController;
    [SerializeField] private float magnitude;

    public Vector3 Value { get; private set; } = Vector3.zero;

    private int prevInput;

    public void SetPreviousInput(InputAction.CallbackContext ctx)
    {
        switch (ctx.phase)
        {
            case InputActionPhase.Canceled:
                prevInput = 0;
                break;
            case InputActionPhase.Started:
                prevInput = 1;
                break;
        }
    }
    
    public void Tick(float deltaTime)
    {
        if (charController.isGrounded)
        {
            Value = new Vector3(0f, prevInput * deltaTime * magnitude, 0f);

            return;
        }

        Value = new Vector3(0f, Mathf.MoveTowards(Value.y, 0f, 0.25f), 0f);
    }
}
