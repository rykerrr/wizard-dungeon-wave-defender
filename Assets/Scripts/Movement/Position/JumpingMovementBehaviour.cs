using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class JumpingMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterController chController = default;
        [SerializeField] private CharacterMovementMotor motor = default;

        [SerializeField] private JumpingMovement jumpMovement = default;

        [SerializeField] private float maxInputMultiplier = 1f;
        [SerializeField] private float inputMultiplier = 0;

        private bool recordInputMultiplier = false;
        
        private void Awake()
        {
            if (chController == null)
            {
                motor = GetComponent<CharacterMovementMotor>();
                chController = GetComponent<CharacterController>();
            }
        }

        private void Update()
        {
            jumpMovement.Tick(Time.fixedDeltaTime, chController.isGrounded);

            RecordInputMultiplierOnJumpHold();
        }

        private void RecordInputMultiplierOnJumpHold()
        {
            if (!recordInputMultiplier) return;

            inputMultiplier += Time.deltaTime;

            if (inputMultiplier > maxInputMultiplier)
            {
                SetPreviousInput(1 + inputMultiplier);
            }
        }

        public void HandleMovementInput(InputAction.CallbackContext ctx)
        {
            if (!chController.isGrounded) return;
            
            if (ctx.phase == InputActionPhase.Started)
            {
                inputMultiplier = 0;
                recordInputMultiplier = true;

                return;
            }

            bool buttonReleasedAfterHold = ctx.phase == InputActionPhase.Canceled && inputMultiplier > 0;
            if (!buttonReleasedAfterHold) return;
            
            recordInputMultiplier = false;

            var newInput = 1 + inputMultiplier; 
            
            SetPreviousInput(newInput);
        }

        private void SetPreviousInput(float input)
        {
            inputMultiplier = 0;
            recordInputMultiplier = false;
            
            jumpMovement.SetPreviousInput(input);
        }
        
        private void OnEnable()
        {
            motor.AddModifier(jumpMovement);
        }

        private void OnDisable()
        {
            motor.RemoveModifier(jumpMovement);
        }
    }
}