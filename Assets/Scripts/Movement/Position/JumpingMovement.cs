using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class JumpingMovement : IMovementModifier
    {
        [SerializeField] private float jumpForce = default;

        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }
        
        public Vector3 Value { get; private set; }

        private float prevInput = 0f;

        public void Tick(float deltaTime, bool isGrounded)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            Vector3 movement = Vector3.zero;
            
            if (isGrounded)
            {
                movement = new Vector3(0f, prevInput * jumpForce * deltaTime);
            }

            Value = movement;
        }

        // Todo: jumping behaviour be based on how long the button is held to an extent
        public void SetPreviousInput(int input)
        {
            prevInput = input;
        }
    }
}
