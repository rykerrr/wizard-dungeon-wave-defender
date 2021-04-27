using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
public class JumpBehavior : MonoBehaviour
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private JumpMovementModifier jumpModifier;
    
    public JumpMovementModifier JumpModifier => jumpModifier;
    
    public void OnEnable() => motor.AddModifier(jumpModifier);
    public void OnDisable() => motor.RemoveModifier(jumpModifier);
    public void FixedUpdate() => jumpModifier.Tick(Time.fixedDeltaTime);
    public void SetPreviousInput(InputAction.CallbackContext ctx) => jumpModifier.SetPreviousInput(ctx);
}
