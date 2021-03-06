using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace WizardGame.Movement.Position
{
    public class ItemGravityBehavior : MonoBehaviour
    {
        [SerializeField] private FloatOverGroundMovement floatMovement = default;
        [SerializeField] private LayerMask whatIsGround = default;
        [SerializeField] private float maxDistanceToGround = default;

        private void Update()
        {
            KeepDistanceToGround();
        }
        
        private void KeepDistanceToGround()
        {
            var position = transform.position;
            bool didHit = Physics.Raycast(position, Vector3.down,
                out RaycastHit hitInfo, maxDistanceToGround, whatIsGround);

            floatMovement.Tick(Time.deltaTime, didHit, hitInfo.distance, maxDistanceToGround);
            position += floatMovement.Value;
            transform.position = position;
        }
    }
}