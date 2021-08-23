using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardGame.Movement.Position
{
    [Serializable]
    public class LocomotionMovement : IMovementModifier
    {
        [SerializeField] private  float mvSpeed = default;

        private float externalMult = 1f;

        public float ExternalMult
        {
            get => externalMult;
            set => externalMult = value;
        }
        
        public float MvSpeed
        {
            get => mvSpeed;
            set => mvSpeed = value;
        }
        
        public Vector3 Value { get; private set; }
        
        private Vector3 previousInput = Vector3.zero;

        public void Tick(float deltaTime)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            Value = previousInput * (mvSpeed * deltaTime) * ExternalMult;
        }
        
        public void SetPreviousMovementInput(Vector2 curInput)
        {
            previousInput = new Vector3(curInput.x, 0f, curInput.y);
        }
    }
}
