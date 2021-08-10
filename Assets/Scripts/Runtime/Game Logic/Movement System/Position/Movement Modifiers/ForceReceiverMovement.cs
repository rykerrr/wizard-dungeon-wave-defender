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

            Value = Vector3.Lerp(Value, Vector3.zero, drag * deltaTime);
        }

        public void AddForce(Vector3 force) => Value += force;
    }
}
