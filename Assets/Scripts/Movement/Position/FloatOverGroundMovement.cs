using System;
using UnityEngine;

namespace WizardGame.Movement.Position
{
    [Serializable]
    public class FloatOverGroundMovement : IMovementModifier
    {
        [SerializeField] private float offset = 0.5f;

        public Vector3 Value { get; private set; }

        public void Tick(bool didHit, float distanceToGround, float maxDistanceToGround)
        {
            if (didHit)
            {
                float distanceToKeep = maxDistanceToGround - offset;

                bool isApproxClose = distanceToGround >= distanceToKeep && distanceToGround <= maxDistanceToGround;
                
                if (isApproxClose)
                {
                    Value = Vector3.zero;
                    return;
                }

                if (distanceToGround > distanceToKeep)
                {
                    Value = Vector3.down * Time.deltaTime;
                }
                else if (distanceToGround < distanceToKeep)
                {
                    Value = Vector3.up * Time.deltaTime;
                }
            }
            else
            {
                // what if it's falling out of the world? and keeps falling down? that'd be an edge case

                Value = Vector3.down * Time.deltaTime;
            }
        }
    }
}