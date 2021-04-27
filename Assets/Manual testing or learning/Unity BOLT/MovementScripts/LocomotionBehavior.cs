using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
public class LocomotionBehavior : MonoBehaviour
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private LocomotionMovementModifier locomotion;

    
    public LocomotionMovementModifier Locomotion => locomotion;

    private void OnEnable() => motor.AddModifier(locomotion);
    private void OnDisable() => motor.RemoveModifier(locomotion);
    private void FixedUpdate() => Locomotion.Tick(Time.fixedDeltaTime);

    public void SetPreviousMovementInput(InputAction.CallbackContext ctx) => Locomotion.SetPreviousMovementInput(ctx);
}
