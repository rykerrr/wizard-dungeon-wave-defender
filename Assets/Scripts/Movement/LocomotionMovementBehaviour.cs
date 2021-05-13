using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public class LocomotionMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private LocomotionMovement locomotion;
        
        public LocomotionMovement Locomotion => locomotion;

        private CharacterController chController;

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
