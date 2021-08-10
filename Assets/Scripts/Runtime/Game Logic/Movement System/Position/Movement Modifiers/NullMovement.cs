using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class NullMovement : IMovementModifier
    {
        public Vector3 Value { get; }
        
        public void Tick(float deltaTime)
        {
            Debug.LogWarning("Using null object");    
        }
    }
}
