using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class JumpingMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private JumpingMovement jumpMovement = default;

        [SerializeField] private float maxInputMultiplier = 1f;
        [SerializeField] private float inputMultiplier = 0;

        private JumpingMovement JumpMovement => jumpMovement;
        
        public override float ExternalMult
        {
            get => JumpMovement.ExternalMult;
            set => JumpMovement.ExternalMult = value;
        }
        
        private bool recordInputMultiplier = false;

        protected override  void Awake()
        {
            base.Awake();
        }

        private void Update()
        {
            RecordInputMultiplierOnJumpHold();
        }

        protected override void FixedUpdate()
        {
            JumpMovement.Tick(Time.fixedDeltaTime, chController.isGrounded);
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

        // set up with unity input system
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
            
            JumpMovement.SetPreviousInput(input);
        }
        
        protected override void OnEnable() => movementMotor.AddModifier(JumpMovement);
        protected override void OnDisable() => movementMotor.RemoveModifier(JumpMovement);
        
    }
}