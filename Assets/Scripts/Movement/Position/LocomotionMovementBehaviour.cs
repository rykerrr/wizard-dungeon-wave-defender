using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class LocomotionMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor = default;
        [SerializeField] private LocomotionMovement locomotion = default;
        
        public LocomotionMovement Locomotion => locomotion;

        private CharacterController chController = default;

        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
            }
            
            chController = GetComponent<CharacterController>();
        }

        private void OnEnable() => movementMotor.AddModifier(locomotion);
        private void OnDisable() => movementMotor.RemoveModifier(locomotion);
        private void FixedUpdate() => Locomotion.Tick(Time.fixedDeltaTime);

        public void SetPreviousMovementInput(InputAction.CallbackContext ctx) =>
            Locomotion.SetPreviousMovementInput(ctx.ReadValue<Vector2>());
    }
}
