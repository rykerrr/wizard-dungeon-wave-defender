using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Item_System.World_Interaction.PhysicalItemMovementMotor
{
    public class FloatOverGroundMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private FloatOverGroundMovement floatMovement = default;
        [SerializeField] private LayerMask whatIsGround = default;
        [SerializeField] private float maxDistanceToGround = 5;

        private FloatOverGroundMovement FloatMovement => floatMovement;
        
        public override float ExternalMult
        {
            get => FloatMovement.ExternalMult;
            set => FloatMovement.ExternalMult = value;
        }
        
        private void RaycastAndTick()
        {
            var position = transform.position;
            bool didHit = Physics.Raycast(position, Vector3.down,
                out RaycastHit hitInfo, maxDistanceToGround, whatIsGround);

            FloatMovement.Tick(Time.deltaTime, didHit, hitInfo.distance, maxDistanceToGround);
        }
        
        protected override void FixedUpdate() => RaycastAndTick();
        protected override void OnEnable() => movementMotor.AddModifier(FloatMovement);
        protected override void OnDisable() =>  movementMotor.RemoveModifier(FloatMovement);
    }
}