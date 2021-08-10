using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class SlidingMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private SlidingMovement slidingMovement = default;
        
        public SlidingMovement SlidingMovement => slidingMovement;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable() => movementMotor.AddModifier(slidingMovement);
        protected override void OnDisable() => movementMotor.RemoveModifier(slidingMovement);
        protected override void FixedUpdate() => slidingMovement.Tick(Time.fixedDeltaTime, transform.position + chController.center);

        public bool ShouldSlide() => slidingMovement.ShouldSlide(transform.position + chController.center, chController.slopeLimit);
    }
}
