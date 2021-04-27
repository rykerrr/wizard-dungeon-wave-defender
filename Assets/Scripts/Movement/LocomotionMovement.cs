using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardGame.Movement
{
    [Serializable]
    public class LocomotionMovement : IMovementModifier
    {
        public Vector3 Value { get; private set; }
        
        private Vector3 previousInput = Vector3.zero;

        public void Tick(float deltaTime, float mvSpeed)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            Value = previousInput * (mvSpeed * deltaTime);
        }
        
        public void SetPreviousMovementInput(Vector2 curInput)
        {
            previousInput = new Vector3(curInput.x, 0f, curInput.y);
        }
    }
}
