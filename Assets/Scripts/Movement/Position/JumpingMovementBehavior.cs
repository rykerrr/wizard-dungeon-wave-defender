using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class JumpingMovementBehavior : MonoBehaviour
    {
        [SerializeField] private ForceReceiverMovementBehaviour forceReceiver = default;
        [SerializeField] private CharacterController charController = default;

        [SerializeField] private float jumpForceMultiplier = 1f;
        
        private void Awake()
        {
            if (charController == null)
            {
                charController = GetComponent<CharacterController>();
            }
        }

        public void SetPreviousMovementInput(InputAction.CallbackContext ctx)
        {
            if (!charController.isGrounded) return;

            forceReceiver.AddForce(new Vector3(0, Convert.ToInt32(ctx.ReadValueAsButton()) * jumpForceMultiplier, 0));
        }
    }
}