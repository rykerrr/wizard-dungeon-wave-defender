using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    [Serializable]
    public class ForceReceiverMovement : IMovementModifier
    {
        [SerializeField] private float drag = default;
        [SerializeField] private float snapForceUnder = default;

        private float externalMult = 1f;

        public float ExternalMult
        {
            get => externalMult;
            set => externalMult = value;
        }
        
        public float Drag
        {
            get => drag;
            set => drag = value;
        }
        
        public Vector3 Value { get; private set; }

        public void Tick(float deltaTime)
        {
            deltaTime = Mathf.Max(deltaTime, 0);
            
            bool isForceTooLow = Value.magnitude < snapForceUnder;

            if (isForceTooLow)
            {
                Value = Vector3.zero;
            }

            Value = Vector3.Lerp(Value, Vector3.zero, drag * deltaTime) * ExternalMult;
        }

        public void AddForce(Vector3 force) => Value += force;
    }
}
