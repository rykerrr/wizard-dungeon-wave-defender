using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class LocomotionMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private LocomotionMovement locomotion = default;
        
        public LocomotionMovement Locomotion => locomotion;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable() => movementMotor.AddModifier(locomotion);
        protected override void OnDisable() => movementMotor.RemoveModifier(locomotion);
        protected override void FixedUpdate() => Locomotion.Tick(Time.fixedDeltaTime);

        public void SetPreviousMovementInput(InputAction.CallbackContext ctx) =>
            Locomotion.SetPreviousMovementInput(ctx.ReadValue<Vector2>());
    }
}
