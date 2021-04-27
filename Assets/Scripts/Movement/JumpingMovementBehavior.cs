using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public class JumpingMovementBehavior : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private JumpingMovement jumpingMovement;

        [SerializeField] private float jumpForce;
        
        public JumpingMovement JumpingMovement => jumpingMovement;

        private CharacterController chConntroller;
        
        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
                chConntroller = GetComponent<CharacterController>();
            }
        }

        private void OnEnable() => movementMotor.AddModifier(jumpingMovement);
        private void OnDisable() => movementMotor.RemoveModifier(jumpingMovement);
        private void FixedUpdate() => jumpingMovement.Tick(Time.fixedDeltaTime, jumpForce, chConntroller.isGrounded);

        public void SetPreviousMovementInput(InputAction.CallbackContext ctx)
            => jumpingMovement.SetPreviousInput(Convert.ToInt32(ctx.ReadValueAsButton()));
    }
}
