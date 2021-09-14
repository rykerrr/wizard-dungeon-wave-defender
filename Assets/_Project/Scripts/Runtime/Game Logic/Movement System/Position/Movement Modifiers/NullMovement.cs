using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class NullMovement : IMovementModifier
    {
        private float externalMult = 1f;

        public float ExternalMult
        {
            get => externalMult;
            set => externalMult = value;
        }
        
        public Vector3 Value { get; }
        
        public void Tick(float deltaTime)
        {
            Debug.LogWarning("Using null object");    
        }
    }
}
