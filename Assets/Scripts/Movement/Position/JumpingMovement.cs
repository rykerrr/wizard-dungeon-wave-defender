using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class JumpingMovement : IMovementModifier
    {
        [SerializeField] private float jumpForce = default;
        [SerializeField] private float drag = default;

        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }
        
        public Vector3 Value { get; private set; }

        private float prevInput = default;

        public void Tick(float deltaTime, bool isGrounded)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            if (isGrounded)
            {
                Debug.Log(prevInput);
                Value = new Vector3(0f, prevInput * jumpForce * deltaTime);
            }

            Value = Vector3.Lerp(Value, Vector3.zero, drag * deltaTime);
            
            prevInput = 0;
        }

        public void SetPreviousInput(float input)
        {
            prevInput = input;
        }
    }
}
