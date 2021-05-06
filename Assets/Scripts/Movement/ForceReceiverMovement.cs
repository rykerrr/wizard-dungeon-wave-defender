using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    [Serializable]
    public class ForceReceiverMovement : IMovementModifier
    {
        public Vector3 Value { get; private set; }

        public void Tick(float deltaTime, float drag)
        {
            bool isForceTooLow = Value.magnitude < 0.2f;

            if (isForceTooLow)
            {
                Value = Vector3.zero;
            }

            Value = Vector3.Lerp(Value, Vector3.zero, drag * deltaTime);
        }

        public void AddForce(Vector3 force) => Value += force;
    }
}
